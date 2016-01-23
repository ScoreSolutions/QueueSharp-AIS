
Imports System.Data.SqlClient
Imports Kiosk.Org.Mentalis.Files
Imports System.IO
Imports Engine.Common.ShopConnectDBENG
Imports Engine.Kiosk.KioskModule

Module Kiosk

    Public Const SoftwareName As String = "Kiosk"
    Public INIFileName As String = Application.StartupPath & "\Kiosk.ini"  'Parth ที่ใช้เก็บไฟล์ .ini
    
    
#Region "All Error"

    Sub showFormError(ByVal Message As String)
        Dim f As New frmErrorMessage
        f.txtMassage.Text = Message
        f.ShowDialog()
        f.Dispose()
    End Sub

#End Region

    Function ShowDialogBox(ByVal Text As String, ByVal HeadText As String, Optional ByVal btnYesNo As Boolean = False) As Boolean
        'Dim f As New frmDialogMsg
        If btnYesNo = True Then
            frmDialogMsg.btnOK.Visible = False
            frmDialogMsg.btnPrevious.Visible = False
            frmDialogMsg.btnMain.Visible = False
            frmDialogMsg.btnConfirm.Visible = True
            frmDialogMsg.btnCancel.Visible = True
        Else
            frmDialogMsg.btnConfirm.Visible = False
            frmDialogMsg.btnCancel.Visible = False
            frmDialogMsg.btnOK.Visible = False
            frmDialogMsg.btnPrevious.Visible = False
            frmDialogMsg.btnMain.Visible = True
            frmDialogMsg.btnMain.Text = msgDialogBtnOK
        End If
        frmDialogMsg.Text = HeadText
        frmDialogMsg.lblText.Text = Text
        If frmDialogMsg.ShowDialog() = Windows.Forms.DialogResult.Yes Then
            Return True
        End If
        Return False
    End Function

    Sub CheckFileList()
        If File.Exists(Application.StartupPath & "\Kiosk.ini") = False Then
            ShowDialogBox("ไม่พบไฟล์ Kiosk.ini", msgWarning)
            Application.Exit()
        End If

        If File.Exists(Application.StartupPath & "\AIS.wmv") = False Then
            ShowDialogBox("ไม่พบไฟล์ AIS.wmv", msgWarning)
            Application.Exit()
        End If

        If File.Exists(Application.StartupPath & "\alert.png") = False Then
            ShowDialogBox("ไม่พบไฟล์ alert.png", msgWarning)
            Application.Exit()
        End If

        If File.Exists(Application.StartupPath & "\Logo_EN.bmp") = False Then
            ShowDialogBox("ไม่พบไฟล์ Logo_EN.bmp", msgWarning)
            Application.Exit()
        End If

        If File.Exists(Application.StartupPath & "\Logo_TH.bmp") = False Then
            ShowDialogBox("ไม่พบไฟล์ Logo_TH.bmp", msgWarning)
            Application.Exit()
        End If
    End Sub

    Sub LoadConfig()
        Dim ChkDB As String = CheckConnDbServer(INIFileName)
        If ChkDB.Trim <> "" Then
            ShowDialogBox(ChkDB, msgWarning)
        End If

        Dim CurrDB As String = GetShopConfig(INIFileName, "CurrentDB", SoftwareName, "LoadConfig")
        If CurrDB.Trim = "" Then
            ShowDialogBox("ไม่พบการตั้งค่า  CurrentDB ใน TB_SETTING", msgWarning)
            Application.Exit()
        End If

        CheckFileList()

        If CheckUpdateConfig(INIFileName, SoftwareName, "Kiosk_LoadConfig") = False Then
            ShowDialogBox(Engine.Kiosk.KioskModule.ErrorMessage, msgWarning)
            Application.Exit()
        End If
    End Sub

    Public Sub showNotify(ByVal Title As String, ByVal text As String)
        Dim ntfy As New TaskBarNotifier
        With ntfy
            .CloseButtonClickEnabled = True
            .TitleClickEnabled = False
            .TextClickEnabled = False
            .DrawTextFocusRect = True
            .KeepVisibleOnMouseOver = True
            .ReShowOnMouseOver = True
            .TransparencyKey = Color.PaleTurquoise

            Dim Image As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\alert.png")
            Dim Image1 As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\close.bmp")
            .SetBackgroundBitmap(Image, Color.FromArgb(255, 0, 255))
            '.SetCloseBitmap(Image1, Color.FromArgb(255, 0, 255), New Point(225, 34))
            .TitleRectangle = New Rectangle(80, 25, 300, 30)
            .TextRectangle = New Rectangle(-65, -40, 400, 300)
            .Show(Title, text, 500, 2000, 1000)
        End With
    End Sub

    Sub CheckNotify()
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim MaxQ As Integer = ini.ReadInteger("MaxQ")
        Dim Ticket As Integer = ini.ReadInteger("Ticket") + 1
        ini.Write("Ticket", Ticket)

        Dim Per As Integer = 100 - (((Ticket) / MaxQ) * 100)
        If Per <= 3 Then
            showNotify("", "กรุณาเปลี่ยนกระดาษ")
        End If
        ini = Nothing
    End Sub
    Public Function SendImageFile(ByVal MobileNo As String, ByVal QueueNo As String, ByVal AssignTime As DateTime) As Long
        Try
            Dim eng As New Engine.Kiosk.KioskCaptureENG
            Dim Path As String = eng.GetCapturePath 'ConfigINI.GetValue("CaptureImage", "Path", "")
            Dim _file As String = Path & MobileNo & ".jpg"
            If IO.File.Exists(_file) = True Then
                Try
                    Dim ws As New ShopWebServiceMain.WebServiceAPI()
                    ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmKioskCapture_SendImageFile")
                    Dim ret As Long = ws.SendCaptureImageFile(IO.File.ReadAllBytes(_file), MobileNo, QueueNo, AssignTime, IO.File.GetCreationTime(_file))
                    If ret > 0 Then
                        System.IO.File.Delete(_file)
                    End If
                    ws = Nothing
                    Return ret
                Catch ex As Exception
                    If IO.Directory.Exists(Path & "Backup") = False Then
                        IO.Directory.CreateDirectory(Path & "Backup")
                    End If
                    IO.File.Move(_file, eng.GetCaptureBackupPath & MobileNo & ".jpg")
                    eng.WriteBackupCaptureIndexFile(MobileNo, QueueNo, AssignTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")))
                End Try
            End If
            eng = Nothing
        Catch ex As Exception
            frmDialogMsg.lblText.Text = ex.Message.ToString
            frmDialogMsg.ShowDialog()
        End Try

    End Function
End Module
