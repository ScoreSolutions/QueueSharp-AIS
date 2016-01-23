Imports Engine.Common

Public Class Form1

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "AIS Siebel Create Activity Confirm Agent V " & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Siebel Create Activity Confirm Agent V " & getMyVersion()
        Dim IsConnect As Boolean = False
        Do
            IsConnect = FunctionEng.CheckShopConnect()
        Loop Until IsConnect = True
        Me.WindowState = FormWindowState.Minimized

        If IsConnect = True Then
            'code for start from

            Timer1.Start()

        End If
    End Sub

    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Try
            '# ตรวจสอบ TB_LOGFILE ที่มีการ confirm
            Dim sql As String = " select * from ( " & _
            " select * from TB_LOGFILE l where  convert(varchar(8),log_date,112) = convert(varchar(8),GETDATE(),112) " & _
            " and status=2 and item_id <> 0 and isnull(l.customer_id,'')<>'' and id not in " & _
            " (select tb_logfile_id from TB_SIEB_CREATE_ACT_CONF_LOG " & _
            " Where convert(varchar(8),log_date,112) = convert(varchar(8),GETDATE(),112)) " & _
            " and queue_no not in " & _
            " (select distinct queue_no from TB_SIEB_CREATE_ACT_CONF_LOG " & _
            " Where convert(varchar(8),log_date,112) = convert(varchar(8),GETDATE(),112))) T1 " & _
            " Union " & _
            " (select * from TB_LOGFILE l where  convert(varchar(8),log_date,112) = convert(varchar(8),GETDATE(),112) " & _
            " and status=2 and item_id = 0 and isnull(l.customer_id,'')<>'' and id not in " & _
            "(select tb_logfile_id from TB_SIEB_CREATE_ACT_CONF_LOG " & _
            " Where convert(varchar(8),log_date,112) = convert(varchar(8),GETDATE(),112))) "
            Dim dt As New DataTable

            dt = FunctionEng.GetShopDataTable(Sql)
            If dt.Rows.Count > 0 Then

                For cnt As Integer = 0 To dt.Rows.Count - 1
                    Dim MobileNo As String = dt.Rows(cnt).Item("customer_id").ToString
                    Dim LogFile_ID As String = dt.Rows(cnt).Item("id").ToString
                    Dim Queue_No As String = dt.Rows(cnt).Item("Queue_No").ToString
                    Dim Counter_ID As String = dt.Rows(cnt).Item("Counter_ID").ToString
                    Dim User_ID As String = dt.Rows(cnt).Item("User_ID").ToString

                    '# ถ้ามีให้ไป Insert Table  TB_SIEB_CREATE_ACT_CONF_LOG
                    Sql = "Insert Into TB_SIEB_CREATE_ACT_CONF_LOG" & _
                    "(id,create_by,create_date,tb_logfile_id,log_date,queue_no,mobile_no,counter_id,user_id,recall_qty) " & _
                    " Values((Select ISNULL(MAX(ID),0) +1 From TB_SIEB_CREATE_ACT_CONF_LOG)" & _
                    " ,'SiebelCreateActivityConfirm',GETDATE(),'" & LogFile_ID & "',GETDATE(),'" & Queue_No & "','" & MobileNo & _
                    "','" & Counter_ID & "','" & User_ID & "',0 )"
                    FunctionEng.ExecuteShopNonQuery(Sql)

                    '# เรียก Web Service SiebelCreateActivityConfirm  เพื่อเอาค่า activity_id 
                    Dim ws As New Engine.CallWebService.ShopWebServiceENG
                    Dim p As New ShParaDb.ShopWebService.SiebelResponsePara
                    p = ws.SiebelCreateActivityConfirm(MobileNo)
                    If p.RESULT Then
                        'ถ้าสำเร็จให้ update
                        'TB_SIEB_CREATE_ACT_CONF_LOG.activity_id, TB_SIEB_CREATE_ACT_CONF_LOG.create_status=1
                        Sql = "Update TB_SIEB_CREATE_ACT_CONF_LOG Set activity_id ='" & p.ACTIVITY_ID & _
                        "',create_status='1',update_by = 'SiebelCreateActivityConfirm' ,update_date = GETDATE() Where tb_logfile_id='" & LogFile_ID & "'"
                        FunctionEng.ExecuteShopNonQuery(Sql)
                    Else
                        'ถ้าไม่สำเร็จให้ update
                        'TB_SIEB_CREATE_ACT_CONF_LOG.create_status=3
                        Sql = "Update TB_SIEB_CREATE_ACT_CONF_LOG Set create_status='3' ,update_by = 'SiebelCreateActivityConfirm' ," & _
                        " update_date = GETDATE(),error_desc='" & p.ErrorMessage & "' Where tb_logfile_id='" & LogFile_ID & "'"
                        FunctionEng.ExecuteShopNonQuery(Sql)
                    End If
                    p = Nothing
                    ws = Nothing
                Next 'End for DT
            End If

            '# จากนั้น คิวรี่เอา mobile_no จาก TB_SIEB_CREATE_ACT_CONF_LOG ที่  create_status=3 and recall_qty<5
            'เพื่อเรียก เรียก Web Service SiebelCreateActivityConfirm  เพื่อเอาค่า activity_id
            Dim IsContinue As Boolean = True
            While IsContinue = True
                Sql = "select tb_logfile_id,mobile_no from TB_SIEB_CREATE_ACT_CONF_LOG where create_status='3' and recall_qty < 5"
                Dim dtMobileNo As New DataTable
                dtMobileNo = FunctionEng.GetShopDataTable(Sql)
                If dtMobileNo.Rows.Count > 0 Then
                    For cntMobile As Integer = 0 To dtMobileNo.Rows.Count - 1
                        Dim _MobileNo As String = dtMobileNo.Rows(cntMobile).Item("mobile_no").ToString
                        Dim _LogFile_ID As String = dtMobileNo.Rows(cntMobile).Item("tb_logfile_id").ToString

                        Dim ws As New Engine.CallWebService.ShopWebServiceENG
                        Dim p As New ShParaDb.ShopWebService.SiebelResponsePara
                        p = ws.SiebelCreateActivityConfirm(_MobileNo)
                        If p.RESULT Then
                            'ถ้าสำเร็จ  update ลงใน 
                            'TB_SIEB_CREATE_ACT_CONF_LOG.activity_id, TB_SIEB_CREATE_ACT_CONF_LOG.create_status=1
                            Sql = "Update TB_SIEB_CREATE_ACT_CONF_LOG Set activity_id ='" & p.ACTIVITY_ID & _
                            "',create_status='1',update_by = 'SiebelCreateActivityConfirm' ,update_date = GETDATE() Where tb_logfile_id='" & _LogFile_ID & "'"
                            FunctionEng.ExecuteShopNonQuery(Sql)
                        Else
                            'ถ้าไม่สำเร็จ
                            'TB_SIEB_CREATE_ACT_CONF_LOG.create_status=3, TB_SIEB_CREATE_ACT_CONF_LOG.recall_qty  +1
                            Sql = "Update TB_SIEB_CREATE_ACT_CONF_LOG Set create_status='3',recall_qty = recall_qty + 1 ,update_by = 'SiebelCreateActivityConfirm' " & _
                            " , update_date = GETDATE() ,error_desc='" & p.ErrorMessage & "' Where tb_logfile_id='" & _LogFile_ID & "'"
                            FunctionEng.ExecuteShopNonQuery(Sql)
                        End If
                        ws = Nothing
                        p = Nothing
                        '# ทำซ้ำ จนกระทั่ง TB_SIEB_CREATE_ACT_CONF_LOG.recall_qty=5
                    Next
                Else
                    IsContinue = False
                End If
            End While
        Catch ex As Exception

        End Try
        Timer1.Enabled = True
    End Sub
End Class
