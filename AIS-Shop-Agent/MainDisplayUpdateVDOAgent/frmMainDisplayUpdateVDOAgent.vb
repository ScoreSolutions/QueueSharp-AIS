Imports System.IO
Imports Engine.Common

Public Class frmMainDisplayUpdateVDOAgent

    'Private Sub frmMainDisplayUpdateVDOAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Me.WindowState = FormWindowState.Minimized
    '    Me.Hide()
    '    Try
    '        Dim ShopAbb As String = Engine.Common.FunctionEng.GetConfig("ShopAbbCode")
    '        Dim ws As New EquipmentFileService.EquipmentFileService
    '        ws.Timeout = 120000
    '        ws.Url = Engine.Common.FunctionEng.GetConfig("EquipmentURL1")
    '        Dim dt As DataTable = ws.GetFileFromDC(DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")), ShopAbb, "1")
    '        If dt.Rows.Count > 0 Then
    '            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
    '            ini.Section = "Setting"
    '            Dim MainDisplayPath As String = ini.ReadString("MainDisplayPath")
    '            If MainDisplayPath.EndsWith("/") = False And MainDisplayPath.EndsWith("\") = False Then
    '                MainDisplayPath &= "\"
    '            End If

    '            Dim MainDisplayMass As String = MainDisplayPath & "FlashFileMass\"
    '            If Directory.Exists(MainDisplayMass) = False Then
    '                Directory.CreateDirectory(MainDisplayMass)
    '            End If

    '            Dim FileTargetType1 As String = ini.ReadString("TargetType1")
    '            Dim FilePath As String = MainDisplayMass & FileTargetType1
    '            For Each dr As DataRow In dt.Rows
    '                If File.Exists(FilePath) = True Then
    '                    File.SetAttributes(FilePath, FileAttributes.Normal)
    '                    File.Delete(FilePath)
    '                End If

    '                Dim dataByte() As Byte
    '                dataByte = CType(dr("Files"), Byte())
    '                Dim fs As FileStream
    '                fs = New FileStream(FilePath, FileMode.CreateNew)
    '                fs.Write(dataByte, 0, dataByte.Length)
    '                fs.Close()

    '                If ws.UpdateTransferStatus(dr("FileScheduleID")) = True Then
    '                    Dim prc() As Process = Process.GetProcessesByName("MainDisplayFlash")
    '                    If prc.Length > 0 Then
    '                        For Each p As Process In prc
    '                            p.Kill()
    '                        Next
    '                    End If

    '                    prc = Process.GetProcessesByName("run_flash_main")
    '                    If prc.Length > 0 Then
    '                        For Each p As Process In prc
    '                            p.Kill()
    '                        Next
    '                    End If
    '                    prc = Process.GetProcessesByName("run_flash_serenade_club")
    '                    If prc.Length > 0 Then
    '                        For Each p As Process In prc
    '                            p.Kill()
    '                        Next
    '                    End If
    '                    prc = Process.GetProcessesByName("run_flash_2nd")
    '                    If prc.Length > 0 Then
    '                        For Each p As Process In prc
    '                            p.Kill()
    '                        Next
    '                    End If

    '                    Process.Start(MainDisplayPath & "MainDisplayFlash.exe")
    '                End If
    '            Next
    '            ini = Nothing
    '        End If
    '        dt.Dispose()
    '        ws = Nothing
    '    Catch ex As Exception

    '    End Try
    '    Application.Exit()
    'End Sub

    Private Sub frmMainDisplayUpdateVDOAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
        'Me.Hide()
        Try
            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim MainDisplayPath As String = ini.ReadString("MainDisplayPath")

            Dim DbIni As IniReader
            Dim IsConnect As Boolean = False
            Do
                DbIni = New IniReader(MainDisplayPath & "Maindisplay.ini")
                DbIni.Section = "Setting"
                Dim DbServerName As String = DbIni.ReadString("Server")
                Dim DbUserID As String = DbIni.ReadString("Username")
                Dim DbPasswd As String = Engine.Common.FunctionEng.DeCripPwd(DbIni.ReadString("Password"))
                Dim DbName As String = DbIni.ReadString("Database")
                IsConnect = FunctionEng.CheckShopConnect(DbServerName, DbUserID, DbPasswd, DbName)
            Loop Until IsConnect = True

            If IsConnect = True Then
                Dim ShopAbb As String = ShopConnectDBENG.GetShopConfig(DbIni.Filename, "ShopAbbCode", "MainDisplayUpdateVDOAgent", "frmMainDisplayUpdateVDOAgent_Load")
                Dim ws As New EquipmentFileService.EquipmentFileService
                ws.Timeout = 120000
                ws.Url = ShopConnectDBENG.GetShopConfig(DbIni.Filename, "EquipmentURL1", "MainDisplayUpdateVDOAgent", "frmMainDisplayUpdateVDOAgent_Load")
                Dim dt As DataTable = ws.GetListFileInfoFromDC(DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")), ShopAbb)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "Target_Type in(1,8)"
                    If MainDisplayPath.EndsWith("/") = False And MainDisplayPath.EndsWith("\") = False Then
                        MainDisplayPath &= "\"
                    End If

                    Dim MainDisplayMass As String = MainDisplayPath & "FlashFileMass\"
                    If Directory.Exists(MainDisplayMass) = False Then
                        Directory.CreateDirectory(MainDisplayMass)
                    End If

                    Dim FileTargetType1 As String = ini.ReadString("TargetType1")
                    Dim FileTargetType2 As String = ini.ReadString("TargetType2")
                    Dim FilePath As String = ""

                    Dim mEng As New Engine.MainDisplay.MainDisplayENG
                    mEng.KillMainDisplayProcess()
                    mEng = Nothing

                    Dim IsStartProcess As Boolean = True

                    For Each dr As DataRowView In dt.DefaultView
                        IsStartProcess = True
                        Dim dataDt As New DataTable
                        Select Case dr("Target_Type")
                            Case "1"
                                FilePath = MainDisplayMass & FileTargetType1
                            Case "8"
                                Dim logopath As String = MainDisplayMass & "logo\"
                                If Directory.Exists(logopath) = False Then
                                    Directory.CreateDirectory(logopath)
                                End If
                                FilePath = logopath & FileTargetType2
                        End Select

                        dataDt = ws.GetFileByteStreamFromDC(dr("Folder_Name") & "\" & dr("File_Name"))
                        If dataDt.Rows.Count > 0 Then
                            If File.Exists(FilePath) = True Then
                                Try
                                    File.SetAttributes(FilePath, FileAttributes.Normal)
                                    File.Delete(FilePath)
                                Catch ex As Exception
                                    Engine.Common.FunctionEng.SaveShopErrorLog("frmMainDisplayUpdateVDOAgent.Shown", "Delete File " & FilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace)
                                End Try
                            End If

                            Try
                                Dim fs As FileStream
                                fs = New FileStream(FilePath, FileMode.CreateNew)
                                For Each fDr As DataRow In dataDt.Rows
                                    Dim b() As Byte = Convert.FromBase64String(fDr("CharData"))
                                    fs.Write(b, 0, b.Length)
                                Next
                                fs.Close()

                                If ws.UpdateTransferStatus(dr("id")) = False Then
                                    IsStartProcess = False
                                End If
                            Catch ex As Exception
                                Engine.Common.FunctionEng.SaveShopErrorLog("frmMainDisplayUpdateVDOAgent.Shown", "Create File Stream " & FilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        End If

                        dataDt.Dispose()
                        If IsStartProcess = False Then
                            Application.Exit()
                        End If
                    Next


                    Try
                        Process.Start(MainDisplayPath & "MainDisplayFlash.exe")
                    Catch ex As Exception
                        Engine.Common.FunctionEng.SaveShopErrorLog("frmMainDisplayUpdateVDOAgent.Shown", "Start MainDisplayFlash " & FilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace)
                    End Try

                End If
                dt.Dispose()
                ws = Nothing
                ini = Nothing
                DbIni = Nothing
            End If
        Catch ex As Exception
            'MessageBox.Show("MainDisplayUpdateVDOAgent Exception :" & ex.Message & vbCrLf & ex.StackTrace)
            Engine.Common.FunctionEng.SaveShopErrorLog("frmMainDisplayUpdateVDOAgent.frmMainDisplayUpdateVDOAgent_Shown", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Application.Exit()
    End Sub

End Class