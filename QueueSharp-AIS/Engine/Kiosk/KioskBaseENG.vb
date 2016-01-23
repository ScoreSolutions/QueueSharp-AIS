Imports System.Windows.Forms
Imports System.IO
Namespace Kiosk
    Public Class KioskBaseENG



#Region "Print"
        Dim _lastPrintY As Integer = 0
        Protected ReadOnly Property lastPrintY() As Integer
            Get
                Return _lastPrintY
            End Get
        End Property

        Protected Sub PrintText(ByVal txt As String, ByVal fnt As System.Drawing.Font, ByVal align As Align, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
            Dim w As Integer = e.Graphics.MeasureString(txt, fnt).Width
            Dim h As Integer = e.Graphics.MeasureString(txt, fnt).Height
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim brsh As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0))
            Select Case align
                Case 0 'Default, LEFT
                    x = e.PageSettings.PrintableArea.Left
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
                Case 1 'CENTER
                    x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
                Case 2 'RIGHT
                    x = e.PageSettings.PrintableArea.Right - w
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
            End Select
            e.Graphics.DrawString(txt, fnt, brsh, x, y)
            'TextRenderer.DrawText(e.Graphics, txt, fnt, New Point(x, y), SystemColors.ControlText)
            _lastPrintY = y + h
        End Sub

        Protected Sub PrintImage(ByVal img As System.Drawing.Image, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
            Dim w As Integer = img.Width
            Dim h As Integer = img.Height
            Dim x As Integer = 0
            Dim y As Integer = 0
            Select Case align
                Case 0 'Default, LEFT
                    x = e.PageSettings.PrintableArea.Left
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
                Case 1 'CENTER
                    x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
                Case 2 'RIGHT
                    x = e.PageSettings.PrintableArea.Right - w
                    y = e.PageSettings.PrintableArea.Top + lastPrintY
            End Select
            e.Graphics.DrawImage(img, x, y)
            _lastPrintY = y + h
            img.Dispose()
        End Sub

        Protected Enum Align As Short
            Left = 0
            Center = 1
            Right = 2
        End Enum
#End Region

        Public Sub UpdateCP(ByVal CAMPAIGN_CODE As String, ByVal CAMPAIGN_NAME As String, ByVal CAMPAIGN_NAME_EN As String, ByVal CAMPAIGN_DESC As String, ByVal CAMPAIGN_DESC_TH2 As String, ByVal CAMPAIGN_DESC_EN2 As String, ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Try
                Dim sql As String = ""
                sql = "update tb_customer set campaign_code = '" & FixDB(CAMPAIGN_CODE) & "',campaign_name = '" & FixDB(CAMPAIGN_NAME) & "',campaign_name_en = '" & FixDB(CAMPAIGN_NAME_EN) & "',campaign_desc = '" & FixDB(CAMPAIGN_DESC) & "',campaign_desc_th2 = '" & FixDB(CAMPAIGN_DESC_TH2) & "',campaign_desc_en2 = '" & FixDB(CAMPAIGN_DESC_EN2) & "' where mobile_no = '" & MobileNo & "'"
                executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            Catch ex As Exception : End Try
        End Sub

        Sub UpdateAR(ByVal ACCOUNT_BALANCE As String, ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Try
                Dim sql As String = ""
                sql = "update tb_customer set account_balance = '" & FixDB(ACCOUNT_BALANCE) & "' where mobile_no = '" & MobileNo & "'"
                executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            Catch ex As Exception : End Try
        End Sub

        Public Sub WriteBackupCaptureIndexFile(ByVal MobileNo As String, ByVal QueueNo As String, ByVal AssignTime As String)
            Dim FileName As String = GetCaptureBackupPath & "BackupCaptureFile.txt"
            Dim objWriter As New System.IO.StreamWriter(FileName, True)
            objWriter.WriteLine(AssignTime & "#" & MobileNo & "#" & QueueNo)
            objWriter.Close()
        End Sub

        Public ReadOnly Property GetCapturePath() As String
            Get
                Dim p As String = Application.StartupPath & "\Capture\"
                If Directory.Exists(p) = False Then
                    Directory.CreateDirectory(p)
                End If
                Return p
            End Get
        End Property

        Public ReadOnly Property GetCaptureBackupPath() As String
            Get
                Dim p As String = GetCapturePath & "Backup\"
                If Directory.Exists(p) = False Then
                    Directory.CreateDirectory(p)
                End If
                Return p
            End Get
        End Property

        Public ReadOnly Property GetCaptureAppointmentPath() As String
            Get
                Dim p As String = GetCapturePath & "Appointment\"
                If Directory.Exists(p) = False Then
                    Directory.CreateDirectory(p)
                End If
                Return p
            End Get
        End Property

        Public Sub New()
            _lastPrintY = 0
        End Sub
    End Class
End Namespace

