Imports System.IO
Imports Engine.Common

Public Class frmDashboardUpdatePictureAgent

    Private Sub frmDashboardUpdatePictureAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Me.WindowState = FormWindowState.Minimized
        'me.hide()
        Try
            Dim ini As New IniReader(Application.StartupPath & "\config.ini")
            ini.Section = "setting"
            Dim dashboradpath As String = ini.ReadString("DashboardPath")

            Dim dbini As IniReader
            Dim isconnect As Boolean = False
            Do
                dbini = New IniReader(dashboradpath & "Dashboard.ini")
                dbini.Section = "setting"
                Dim dbservername As String = dbini.ReadString("server")
                Dim dbuserid As String = dbini.ReadString("username")
                Dim dbpasswd As String = Engine.Common.FunctionEng.DeCripPwd(dbini.ReadString("password"))
                Dim dbname As String = dbini.ReadString("database")
                isconnect = FunctionEng.CheckShopConnect(dbservername, dbuserid, dbpasswd, dbname)
            Loop Until isconnect = True

            If isconnect = True Then
                Dim shopabb As String = ShopConnectDBENG.GetShopConfig(dbini.Filename, "shopabbcode", "maindisplayupdatevdoagent", "frmDashboardUpdatePictureAgent_load")
                Dim ws As New EquipmentFileService.EquipmentFileService
                ws.Timeout = 120000
                ws.Url = ShopConnectDBENG.GetShopConfig(dbini.Filename, "equipmenturl1", "dashboradupdatepictureagent", "frmDashboardUpdatePictureAgent_load")
                Dim dt As DataTable = ws.GetFileInfoFromDC(DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-us")), shopabb, "9")
                If dt.Rows.Count > 0 Then
                    If dashboradpath.EndsWith("/") = False And dashboradpath.EndsWith("\") = False Then
                        dashboradpath &= "\"
                    End If

                    Dim dashboradpathlogo As String = dashboradpath & "logo\"
                    If Directory.Exists(dashboradpathlogo) = False Then
                        Directory.CreateDirectory(dashboradpathlogo)
                    End If

                    Dim filetargettype1 As String = ini.ReadString("targettype1")
                    Dim filepath As String = dashboradpathlogo & filetargettype1

                    Dim meng As New Engine.Dashboard.DashboardENG
                    meng.KillDashboardProcess()
                    meng = Nothing

                    Dim IsStartProcess As Boolean = True

                    For Each dr As DataRow In dt.Rows
                        IsStartProcess = True
                        Dim datadt As New DataTable
                        datadt = ws.GetFileByteStreamFromDC(dr("folder_name") & "\" & dr("file_name"))
                        If datadt.Rows.Count > 0 Then
                            If File.Exists(filepath) = True Then
                                Try
                                    File.SetAttributes(filepath, FileAttributes.Normal)
                                    File.Delete(filepath)
                                Catch ex As Exception
                                    Engine.Common.FunctionEng.SaveShopErrorLog("frmDashboardUpdatePictureAgent.shown", "delete file " & filepath & " exception:" & ex.Message & vbNewLine & ex.StackTrace)
                                End Try
                            End If

                            Try
                                Dim fs As FileStream
                                fs = New FileStream(filepath, FileMode.CreateNew)
                                For Each fdr As DataRow In datadt.Rows
                                    Dim b() As Byte = Convert.FromBase64String(fdr("chardata"))
                                    fs.Write(b, 0, b.Length)
                                Next
                                fs.Close()

                                If ws.UpdateTransferStatus(dr("id")) = False Then
                                    IsStartProcess = False
                                End If
                            Catch ex As Exception
                                Engine.Common.FunctionEng.SaveShopErrorLog("frmDashboardUpdatePictureAgent.shown", "create file stream " & filepath & " exception:" & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        End If

                        datadt.Dispose()

                        If IsStartProcess = False Then
                            Application.Exit()
                        End If
                    Next


                    Try
                        Process.Start(dashboradpath & "Dashboard.exe")
                    Catch ex As Exception
                        Engine.Common.FunctionEng.SaveShopErrorLog("frmDashboardUpdatePictureAgent.shown", "start dashboardflash " & filepath & " exception:" & ex.Message & vbNewLine & ex.StackTrace)
                    End Try

                End If
                dt.Dispose()
                ws = Nothing
                ini = Nothing
                dbini = Nothing
            End If
        Catch ex As Exception
            'messagebox.show("maindisplayupdatevdoagent exception :" & ex.message & vbcrlf & ex.stacktrace)
            Engine.Common.FunctionEng.SaveShopErrorLog("frmDashboardUpdatePictureAgent.frmDashboardUpdatePictureAgent_shown", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Application.Exit()
    End Sub
End Class
