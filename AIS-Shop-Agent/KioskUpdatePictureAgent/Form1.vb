Imports System.IO
Imports Engine.Common

Public Class Form1

    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()

        Dim Inireader As New IniReader(Application.StartupPath & "\Config.ini")
        Inireader.Section = "Setting"
        Dim KioskPath As String = Inireader.ReadString("Path")
        If KioskPath.EndsWith("/") = False And KioskPath.EndsWith("\") = False Then
            KioskPath &= "\"
        End If
        Dim DbIni As IniReader
        
        Dim IsConnect As Boolean = False
        Do
            DbIni = New IniReader(KioskPath & "Kiosk.ini")
            DbIni.Section = "Setting"
            Dim DbServerName As String = DbIni.ReadString("Server")
            Dim DbUserID As String = DbIni.ReadString("Username")
            Dim DbPasswd As String = Engine.Common.FunctionEng.DeCripPwd(DbIni.ReadString("Password"))
            Dim DbName As String = DbIni.ReadString("Database")
            IsConnect = FunctionEng.CheckShopConnect(DbServerName, DbUserID, DbPasswd, DbName)
        Loop Until IsConnect = True

        If IsConnect = True Then
            Try
                ''== updateshopsetting
                Dim TargetType6 As String = Inireader.ReadString("TargetType6") 'Stand
                Dim TargetType7 As String = Inireader.ReadString("TargetType7") 'Elo
                ShopConnectDBENG.UpdateShopConfig(DbIni.Filename, "LogoKioskPath", KioskPath & TargetType6, "KioskUpdatePictureAgent", "Form1_Load")
                ShopConnectDBENG.UpdateShopConfig(DbIni.Filename, "LogoKioskEloPath", KioskPath & TargetType7, "KioskUpdatePictureAgent", "Form1_Load")
                ''End

                Dim ShopAbb As String = ShopConnectDBENG.GetShopConfig(DbIni.Filename, "ShopAbbCode", "KioskUpdatePictureAgent", "Form1_Load")
                Dim ws As New EquipmentFileService.EquipmentFileService
                ws.Timeout = 120000
                ws.Url = ShopConnectDBENG.GetShopConfig(DbIni.Filename, "EquipmentURL1", "KioskUpdatePictureAgent", "Form1_Load")
                Dim dt As New DataTable
                dt = ws.GetListFileInfoFromDC(DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")), ShopAbb)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "Target_Type not in(1,8,9)"
                    For Each dr As DataRowView In dt.DefaultView

                        If ReplacePicture(dr("Target_Type"), Inireader, KioskPath, ws, dr("Folder_Name") & "\" & dr("File_Name")) = True Then
                            ws.UpdateTransferStatus(dr("id"))
                        End If
                    Next
                End If

                ws = Nothing
            Catch ex As Exception
                InsertErrorLog("Exception:" & ex.Message & vbNewLine & ex.StackTrace, Inireader.Filename, "KioskUpdatePictureAgent.Form1.Shown")
            End Try
        End If
        DbIni = Nothing
        Inireader = Nothing

        Application.Exit()
    End Sub

    Function ReplacePicture(ByVal TargetType As String, ByVal Inireader As IniReader, ByVal KioskPath As String, ByVal ws As EquipmentFileService.EquipmentFileService, ByVal DCFilePath As String) As Boolean
        Dim ret As Boolean = True
        Dim fileName As String = ""
        Select Case TargetType
            Case "2"
                fileName = Inireader.ReadString("TargetType2")
            Case "3"
                fileName = Inireader.ReadString("TargetType3")
            Case "4"
                fileName = Inireader.ReadString("TargetType4")
            Case "5"
                fileName = Inireader.ReadString("TargetType5")
            Case "6"
                fileName = Inireader.ReadString("TargetType6")
            Case "7"
                fileName = Inireader.ReadString("TargetType7")
        End Select

      
        If Directory.Exists(KioskPath) = False Then
            Directory.CreateDirectory(KioskPath)
        End If

        Dim FilePath As String = KioskPath & fileName
        Try
            Dim dt As DataTable = ws.GetFileByteStreamFromDC(DCFilePath)
            If dt.Rows.Count > 0 Then
                If File.Exists(FilePath) = True Then
                    Try
                        File.SetAttributes(FilePath, FileAttributes.Normal)
                        File.Delete(FilePath)
                    Catch ex As Exception
                        ret = False
                        InsertErrorLog("Delete Exsiting File KioskFilePath: " & KioskPath & " DCFilePath:" & DCFilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace, Inireader.Filename, "KioskUpdatePictureAgent.Form1.ReplacePicture")
                    End Try
                End If

                Try
                    Dim fs As FileStream
                    fs = New FileStream(FilePath, FileMode.CreateNew)
                    For Each fDr As DataRow In dt.Rows
                        Dim b() As Byte = Convert.FromBase64String(fDr("CharData"))
                        fs.Write(b, 0, b.Length)
                    Next
                    fs.Close()
                Catch ex As Exception
                    ret = False
                    InsertErrorLog("Write FileStream KioskFilePath: " & KioskPath & " DCFilePath:" & DCFilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace, Inireader.Filename, "KioskUpdatePictureAgent.Form1.ReplacePicture")
                End Try
            End If
        Catch ex As Exception
            ret = False
            InsertErrorLog("KioskFilePath: " & KioskPath & " DCFilePath:" & DCFilePath & " Exception:" & ex.Message & vbNewLine & ex.StackTrace, Inireader.Filename, "KioskUpdatePictureAgent.Form1.ReplacePicture")
        End Try

        Return ret
    End Function
    Private Function InsertErrorLog(ByVal ErrMessage As String, ByVal INIFileName As String, ByVal FunctionName As String) As Boolean
        Dim sql As String = "insert into tb_error_log(id,create_by,create_date,"
        sql += " log_date,error_message,client_ip,version) "
        sql += " values((select max(id)+1 from TB_ERROR_LOG),0,getdate(),"
        sql += " getdate(),'" & ErrMessage & "','" & Engine.Common.FunctionEng.GetIPAddress() & "','" & getMyVersion() & "')"

        ShopConnectDBENG.ExecuteNonQuery(sql, INIFileName, "KioskUpdatePictureAgent", FunctionName)

    End Function

    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function
End Class
