Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.IO
Imports AMS
Imports DirectX.Capture
Imports System.Threading
Imports System.Globalization
Imports Microsoft.Win32

Namespace QM
    Module QM
        Public CheckQid As String = "A000"

        Private Const WM_CLOSE = &H10
        Const VK_F As Integer = &H46
        Const VK_CTRL As Integer = &H11
        Const VK_ALT As Integer = &H12
        Const VK_PAUSE As Integer = &H13
        Const WM_SYSKEYDOWN As Integer = &H100
        Const WM_SYSKEYUP As Integer = &H101
        <DllImport("user32.dll")> _
        Public Function FindWindow(ByVal sClassName As String, ByVal sAppName As String) As IntPtr
        End Function
        <DllImport("User32.Dll", EntryPoint:="PostMessageA")> _
        Private Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
        End Function

        Public PathProgram As String = Application.StartupPath & "\QM"
        Public PathFile As String = Application.StartupPath & "\QM\Temp\"

        Sub StartQM(ByVal Queue As String)
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select config_value  from TB_SETTING where config_name = 'q_qm'"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("config_value").ToString = "Y" Then
                        Shell(String.Format("{0}\QualityManagement.exe -QID{1}", PathProgram, Queue))
                    End If
                End If
                dt.Dispose()
            Catch ex As Exception : End Try
        End Sub

        Sub CloseQM()
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select config_value from TB_SETTING where config_name = 'q_qm'"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("config_value").ToString = "Y" Then
                        Dim hWindow As Long = FindWindow(vbNullString, "QM")
                        '"Controller" is the caption of the application running
                        PostMessage(hWindow, WM_SYSKEYDOWN, VK_PAUSE, 0)
                        PostMessage(hWindow, WM_SYSKEYUP, VK_PAUSE, 0)
                    End If
                End If
                dt.Dispose()
            Catch ex As Exception : End Try

        End Sub

        Public Sub WriteTextQueueID(ByVal ValTxt As String)
            Try
                If ValTxt = "" Then
                    ValTxt = " "
                End If
                Dim txtFile As New StreamWriter(PathProgram & "/QM.txt", False, System.Text.Encoding.UTF8, ValTxt.Length)
                With txtFile
                    .Write(ValTxt)
                    .Close()
                End With
            Catch ex As Exception : End Try
        End Sub

        Public Sub WriteTextQueueNo(ByVal QueneNo As String)
            Try
                If QueneNo = "" Then
                    QueneNo = " "
                End If
                Dim txtFile As New StreamWriter(PathProgram & "/TextQueueNo.txt", False, System.Text.Encoding.UTF8, QueneNo.Length)
                With txtFile
                    .Write(QueneNo)
                    .Close()
                End With
            Catch ex As Exception : End Try
        End Sub

        Dim QMScheduleProcID As Integer = 0
        Sub StartQMSchedule()
            Try
                For Each p As Process In Process.GetProcessesByName("QMSchedule")
                    p.Kill()
                Next
                QMScheduleProcID = Shell(Application.StartupPath & "\QMSchedule.exe", AppWinStyle.Hide)
            Catch ex As Exception

            End Try
        End Sub

        Sub StartQMScheduleLogin()
            Try
                For Each p As Process In Process.GetProcessesByName("QMSchedule")
                    p.Kill()
                Next
                QMScheduleProcID = Shell(Application.StartupPath & "\QMSchedule.exe 6", AppWinStyle.Hide)
            Catch ex As Exception

            End Try
        End Sub

        Sub CloseQMSchedule()
            Try
                Dim proc As Process = Process.GetProcessById(QMScheduleProcID)
                proc.Kill()
            Catch ex As Exception

            End Try
        End Sub

        Public Function InsertQmTransferLog(ByVal vStatus As String, ByVal Qid As String) As Long
            Dim ret As Long = 0
            Dim dt As New DataTable
            dt = GetQueueDataByDate(DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")), Qid)
            If dt.Rows.Count = 1 Then
                Dim qDr As DataRow = dt.Rows(0)
                Dim ServiceDate As String = Convert.ToDateTime(qDr("service_date")).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))

                Dim cDt As New DataTable
                Dim cSql As String = "select id from TB_QM_TRANSFER_LOG"
                cSql += " where DATEDIFF(D,GETDATE(),service_date)=0 and tb_counter_queue_id='" & qDr("id") & "'"
                cDt = getDataTable(cSql)
                If cDt.Rows.Count = 0 Then
                    Dim MaxID As String = FindID("TB_QM_TRANSFER_LOG")
                    Dim sql As String = " insert into TB_QM_TRANSFER_LOG (id,create_by,create_date,transfer_date,service_date,tb_counter_queue_id,"
                    sql += " queue_no,mobile_no,item_id,item_name_en,counter_id,counter_name,status, ip_address)"
                    sql += " values('" & MaxID & "','" & qDr("username") & "',getdate(),getdate(),'" & ServiceDate & "','" & qDr("id") & "',"
                    sql += " '" & qDr("queue_no") & "','" & qDr("mobile_no") & "','" & qDr("item_id") & "','" & qDr("item_name") & "','" & qDr("counter_id") & "','" & qDr("counter_name") & "','" & vStatus & "','" & GetIPAddress() & "')"

                    If executeSQL(sql, False) = True Then
                        ret = MaxID
                    End If
                Else
                    Dim sql As String = "update TB_QM_TRANSFER_LOG "
                    sql += " set update_date=getdate(),update_by='" & qDr("username") & "',"
                    sql += " transfer_date=getdate(),counter_id='" & qDr("counter_id") & "',counter_name='" & qDr("counter_name") & "',"
                    sql += " status='" & vStatus & "', ip_address='" & GetIPAddress() & "'"
                    sql += " where id= '" & cDt.Rows(0)("id") & "' "
                    executeSQL(sql, False)

                    ret = Convert.ToInt64(cDt.Rows(0)("id"))
                End If
                cDt.Dispose()
            End If
            dt.Dispose()

            Return ret
        End Function

        Private Function GetQueueDataByDate(ByVal vServiceDate As String, ByVal QueueID As Integer) As DataTable
            Dim dt As New DataTable
            Try
                Dim sql As String = "select q.id ,q.service_date,q.queue_no,q.customer_id mobile_no,u.username,q.item_id,i.item_name,q.counter_id,c.counter_name"
                sql += " from TB_COUNTER_QUEUE q"
                sql += " inner join TB_USER u on u.id=q.user_id"
                sql += " inner join TB_ITEM i on i.id=q.item_id"
                sql += " inner join TB_COUNTER c on c.id=q.counter_id"
                sql += " where convert(varchar(8),q.service_date,112)='" & vServiceDate & "' and q.id=" & QueueID

                dt = getDataTable(sql)
            Catch ex As Exception

            End Try

            Return dt
        End Function

        Public Sub CloseQMProcess()
            For Each p As Process In Process.GetProcessesByName("QualityManagement")
                p.Kill()
            Next
        End Sub
    End Module
End Namespace

