Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports Dashboard.Org.Mentalis.Files

Public Class Form1
    Private Declare Function SetDesktopBackground Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal IpClassName As String, ByVal IpWindowName As String) As IntPtr
    Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As IntPtr
    <DllImport("user32.dll", EntryPoint:="SendMessage", SetLastError:=True)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    Private INIFName As String = Application.StartupPath & "\Dashboard.ini"

    Private Const SW_HIDE As Integer = 0
    Private Const SW_RESTORE As Integer = 9
    Private Sub HideDesktopIcon()
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If hwnd <> 0 Then
            ShowWindow(hwnd, SW_HIDE)
        End If
    End Sub
    Private Sub ShowDesktopIcon()
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If hwnd <> 0 Then
            ShowWindow(hwnd, SW_RESTORE)
        End If
    End Sub

    Const WM_COMMAND As Integer = &H111
    Const MIN_ALL As Integer = 419
    Const MIN_ALL_UNDO As Integer = 416
    Private Sub MinimizeAllWindows()
        Dim lHwnd As IntPtr = FindWindow("Shell_TrayWnd", Nothing)
        SendMessage(lHwnd, WM_COMMAND, MIN_ALL, IntPtr.Zero)
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "OK" Then
            StartDashboard()
        Else
            Try
                'Get the list of processes
                If lblFlashProcessID.Text.Trim <> "" Then
                    Dim myProcess As Process = Process.GetProcessById(lblFlashProcessID.Text)
                    myProcess.Kill()
                End If
            Catch ex As Exception

            End Try
            ShowDesktopIcon()
            System.Environment.Exit(0)
        End If
    End Sub

    Private Sub StartDashboard()
        Dim IsConnect As Boolean = False
        Do
            IsConnect = Engine.Common.ShopConnectDBENG.CheckDBConn(INIFName)
        Loop Until IsConnect = True

        If IsConnect = True Then
            Try
                RefreshTime = Convert.ToInt16(Engine.Common.ShopConnectDBENG.GetShopConfig(INIFName, "DashboardRefreshMinute", "Dashboard", "Form1_StartDashboard"))
                If IO.Directory.Exists(GetXMLFolder) = False Then
                    IO.Directory.CreateDirectory(GetXMLFolder)
                End If
                GeneratePieChart()
                GenerateSummary()
                GenerateService()
                Timer1.Enabled = True

                Dim Pross As New Process
                Pross.StartInfo.FileName = Application.StartupPath & "\AIS-Dashboard.exe"
                Pross.Start()
                lblFlashProcessID.Text = Pross.Id

                Button1.Text = "Exit"
                Me.WindowState = FormWindowState.Minimized
                Me.Hide()
                NotifyIcon1.Visible = True
            Catch ex As IO.IOException

            Catch ex As Exception
                txtErr.Text &= ex.Message & vbCrLf & ex.StackTrace
            End Try
        End If
    End Sub

    Sub GeneratePieChart()
        Dim TempXML As New XDocument
        Dim pieElement As New XElement("pieChart")
        Dim strSql As String = ""
        Try
            pieElement.Add(New XAttribute("titlesColor", "0xFFFFFF"))
            pieElement.Add(New XAttribute("xChart", "0"))
            pieElement.Add(New XAttribute("yChart", "160"))
            pieElement.Add(New XAttribute("wChart", "320"))
            pieElement.Add(New XAttribute("hChart", "160"))
            pieElement.Add(New XAttribute("nbDepth", "30"))
            pieElement.Add(New XAttribute("nbMove", "0.15"))
            pieElement.Add(New XAttribute("showLabels", "TRUE"))
            pieElement.Add(New XAttribute("xLabels", "150"))
            pieElement.Add(New XAttribute("yLabels", "50"))
            pieElement.Add(New XAttribute("ttColor", "0x000000"))
            pieElement.Add(New XAttribute("speed", "0.8"))
            pieElement.Add(New XAttribute("synchonized", "false"))
            pieElement.Add(New XAttribute("esWedge", "Regular.easeIn"))
            pieElement.Add(New XAttribute("esMove", "Regular.easeOut"))

            Dim strTopColor12Call As String = "0x00CC00"
            Dim strTopColorVisitor As String = "0xFF414C"
            Dim strTopColorGSM As String = "0xFFCC33"
            Dim strTopColorSerenade As String = "0x660066"
            Dim strTopColor3G As String = "0x0099CC"

            Dim strlightColor12Call As String = "0x00CC00"
            Dim strlightColorVisitor As String = "0x9F161E"
            Dim strlightColorGSM As String = "0xFFCC33"
            Dim strlightColorSerenade As String = "0x660066"
            Dim strlightColor3G As String = "0x0099CC"

            Dim strfrontColor12Call As String = "0x00FF33"
            Dim strfrontColorVisitor As String = "0xEB1C24"
            Dim strfrontColorGSM As String = "0xFFFF66"
            Dim strfrontColorSerenade As String = "0x663366"
            Dim strfrontColor3G As String = "0x00FFCC"

            Dim strsideColor12Call As String = "0x00FF33"
            Dim strsideColorVisitor As String = "0x6B0206"
            Dim strsideColorGSM As String = "0xFFFF33"
            Dim strsideColorSerenade As String = "0x663366"
            Dim strsideColor3G As String = "0x00FFCC"

            strSql = "SELECT case when c.network_type is null or LTRIM(rtrim(c.network_type))=''  then  'Non-AIS' " & vbNewLine
            strSql += " 	when CHARINDEX('3G',c.network_type)>0 and ct.id<>1 then '3G'" & vbNewLine
            strSql += "     when CHARINDEX('GSM',c.network_type)>0 and ct.id<>1 then 'GSM Advance'" & vbNewLine
            strSql += " 	else c.network_type end as network_type," & vbNewLine
            strSql += " 	case ct.id when 3 then " & vbNewLine
            strSql += " 		case when (select top 1 id from TB_SEGMENT where segment=cq.segment)>0 then 'Serenade' else 'Mass' end" & vbNewLine
            strSql += " 	else ct.customertype_name end customertype_name, " & vbNewLine
            strSql += " count(distinct cq.queue_no) Total" & vbNewLine
            strSql += " FROM TB_COUNTER_QUEUE cq" & vbNewLine
            strSql += " left join [TB_CUSTOMER] c on c.mobile_no=cq.customer_id " & vbNewLine
            strSql += " inner join TB_CUSTOMERTYPE ct on ct.id=cq.customertype_id " & vbNewLine
            strSql += " where Convert(varchar(8), service_date, 112) = Convert(varchar(8), getdate(), 112) " & vbNewLine
            strSql += " group by case when c.network_type is null or LTRIM(rtrim(c.network_type))='' then  'Non-AIS' " & vbNewLine
            strSql += " 	when CHARINDEX('3G',c.network_type)>0 and ct.id<>1 then '3G'" & vbNewLine
            strSql += "     when CHARINDEX('GSM',c.network_type)>0 and ct.id<>1 then 'GSM Advance'" & vbNewLine
            strSql += " 	else c.network_type end, " & vbNewLine
            strSql += " ct.customertype_name, ct.id,ct.customertype_name,cq.segment" & vbNewLine

            Dim Dt As New DataTable
            Dt = Engine.Common.ShopConnectDBENG.ExecuteTable(strSql, INIFName, "Dashboard", "Form1_GeneratePieChart")
            If Dt.Rows.Count > 0 Then
                Dim dblTotal As Double = 0
                For Each Drow As DataRow In Dt.Rows
                    dblTotal += Convert.ToDouble(Drow("total"))
                Next

                'Non AIS
                Dt.DefaultView.RowFilter = "network_type = '' or network_type='Non-AIS'"
                Dim TempElement As New XElement("serie")
                TempElement.Add(New XAttribute("name", "Non-AIS"))
                If Dt.DefaultView.Count > 0 Then
                    TempElement.Add(New XAttribute("value", Dt.DefaultView(0)("Total")))
                Else
                    TempElement.Add(New XAttribute("value", 0))
                End If
                TempElement.Add(New XAttribute("topColor", strTopColor12Call))
                TempElement.Add(New XAttribute("lightColor", strlightColor12Call))
                TempElement.Add(New XAttribute("frontColor", strfrontColor12Call))
                TempElement.Add(New XAttribute("sideColor", strsideColor12Call))
                pieElement.Add(TempElement)

                'One 2 Call
                Dt.DefaultView.RowFilter = "network_type = 'One-2-Call' and customertype_name='MASS'"
                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "One-2-Call"))
                If Dt.DefaultView.Count > 0 Then
                    TempElement.Add(New XAttribute("value", Dt.DefaultView(0)("Total")))
                Else
                    TempElement.Add(New XAttribute("value", 0))
                End If
                TempElement.Add(New XAttribute("topColor", strTopColorVisitor))
                TempElement.Add(New XAttribute("lightColor", strlightColorVisitor))
                TempElement.Add(New XAttribute("frontColor", strfrontColorVisitor))
                TempElement.Add(New XAttribute("sideColor", strsideColorVisitor))
                pieElement.Add(TempElement)

                'GSM Advance
                Dt.DefaultView.RowFilter = "network_type = 'GSM Advance' and customertype_name='MASS'"
                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "GSM Advance"))
                If Dt.DefaultView.Count > 0 Then
                    Dim GsmTotal As Long = 0
                    For Each dr As DataRowView In Dt.DefaultView
                        GsmTotal += Convert.ToInt64(dr("Total"))
                    Next
                    TempElement.Add(New XAttribute("value", GsmTotal))
                Else
                    TempElement.Add(New XAttribute("value", 0))
                End If
                TempElement.Add(New XAttribute("topColor", strTopColorGSM))
                TempElement.Add(New XAttribute("lightColor", strlightColorGSM))
                TempElement.Add(New XAttribute("frontColor", strfrontColorGSM))
                TempElement.Add(New XAttribute("sideColor", strsideColorGSM))
                pieElement.Add(TempElement)

                'Serenade
                Dt.DefaultView.RowFilter = "customertype_name = 'Serenade' "
                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "Serenade"))
                If Dt.DefaultView.Count > 0 Then
                    Dim vSerenade As Integer = 0
                    For Each dr As DataRowView In Dt.DefaultView
                        vSerenade += Convert.ToDouble(dr("Total"))
                    Next
                    TempElement.Add(New XAttribute("value", vSerenade))
                Else
                    TempElement.Add(New XAttribute("value", 0))
                End If
                TempElement.Add(New XAttribute("topColor", strTopColorSerenade))
                TempElement.Add(New XAttribute("lightColor", strlightColorSerenade))
                TempElement.Add(New XAttribute("frontColor", strfrontColorSerenade))
                TempElement.Add(New XAttribute("sideColor", strsideColorSerenade))
                pieElement.Add(TempElement)

                '3G
                Dt.DefaultView.RowFilter = "network_type = '3G' and customertype_name='MASS' "
                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "3G"))
                If Dt.DefaultView.Count > 0 Then
                    TempElement.Add(New XAttribute("value", Dt.DefaultView(0)("Total")))
                Else
                    TempElement.Add(New XAttribute("value", 0))
                End If
                TempElement.Add(New XAttribute("topColor", strTopColor3G))
                TempElement.Add(New XAttribute("lightColor", strlightColor3G))
                TempElement.Add(New XAttribute("frontColor", strfrontColor3G))
                TempElement.Add(New XAttribute("sideColor", strsideColor3G))
                pieElement.Add(TempElement)

                TempElement = Nothing
            Else
                Dim TempElement As New XElement("serie")
                TempElement.Add(New XAttribute("name", "Non-AIS"))
                TempElement.Add(New XAttribute("value", 20))
                TempElement.Add(New XAttribute("topColor", strTopColor12Call))
                TempElement.Add(New XAttribute("lightColor", strlightColor12Call))
                TempElement.Add(New XAttribute("frontColor", strfrontColor12Call))
                TempElement.Add(New XAttribute("sideColor", strsideColor12Call))
                pieElement.Add(TempElement)

                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "One-2-Call"))
                TempElement.Add(New XAttribute("value", 20))
                TempElement.Add(New XAttribute("topColor", strTopColorVisitor))
                TempElement.Add(New XAttribute("lightColor", strlightColorVisitor))
                TempElement.Add(New XAttribute("frontColor", strfrontColorVisitor))
                TempElement.Add(New XAttribute("sideColor", strsideColorVisitor))
                pieElement.Add(TempElement)

                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "GSM Advance"))
                TempElement.Add(New XAttribute("value", 20))
                TempElement.Add(New XAttribute("topColor", strTopColorGSM))
                TempElement.Add(New XAttribute("lightColor", strlightColorGSM))
                TempElement.Add(New XAttribute("frontColor", strfrontColorGSM))
                TempElement.Add(New XAttribute("sideColor", strsideColorGSM))
                pieElement.Add(TempElement)

                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "Serenade"))
                TempElement.Add(New XAttribute("value", 20))
                TempElement.Add(New XAttribute("topColor", strTopColorSerenade))
                TempElement.Add(New XAttribute("lightColor", strlightColorSerenade))
                TempElement.Add(New XAttribute("frontColor", strfrontColorSerenade))
                TempElement.Add(New XAttribute("sideColor", strsideColorSerenade))
                pieElement.Add(TempElement)

                TempElement = New XElement("serie")
                TempElement.Add(New XAttribute("name", "3G"))
                TempElement.Add(New XAttribute("value", 20))
                TempElement.Add(New XAttribute("topColor", strTopColor3G))
                TempElement.Add(New XAttribute("lightColor", strlightColor3G))
                TempElement.Add(New XAttribute("frontColor", strfrontColor3G))
                TempElement.Add(New XAttribute("sideColor", strsideColor3G))
                pieElement.Add(TempElement)
                TempElement = Nothing
            End If
            Dt.Dispose()

            TempXML.Add(pieElement)
            TempXML.Save(GetXMLFolder() & "/pieChart.xml")
            TempXML = Nothing
        Catch ex As IO.IOException

        Catch ex As Exception
            txtErr.Text &= ex.Message & vbCrLf & ex.StackTrace
        Finally
            TempXML = Nothing
            pieElement = Nothing
        End Try
    End Sub

    Sub GenerateService()
        Dim TempXML As New XDocument
        Dim NewElement As New XElement("NewDataSet")
        Dim strSql As String = ""
        Try
            strSql = "exec SP_DASHBOARD_SERVICE"
            Dim dt As New DataTable
            dt = Engine.Common.ShopConnectDBENG.ExecuteTable(strSql, INIFName, "MainDisplayFlash", "Form1_GenerateService")
            If dt.Rows.Count > 0 Then
                For Each Dr As DataRow In dt.Rows
                    Dim SummaryElement As New XElement("SERVICE_LIST")
                    SummaryElement.Add(New XElement("id", Dr.Item("id")))
                    SummaryElement.Add(New XElement("item_name", Dr.Item("item_name")))
                    SummaryElement.Add(New XElement("item_name_th", Dr.Item("item_group_name_th")))
                    SummaryElement.Add(New XElement("kpi_percen", Math.Round(Dr.Item("kpi_percen"), MidpointRounding.AwayFromZero)))
                    SummaryElement.Add(New XElement("color", Dr.Item("color")))
                    SummaryElement.Add(New XElement("csi_percen", Math.Round(Dr.Item("csi_percen"), MidpointRounding.AwayFromZero)))
                    SummaryElement.Add(New XElement("csi_face", Dr.Item("csi_face")))
                    NewElement.Add(SummaryElement)
                    SummaryElement = Nothing
                Next
                TempXML.Add(NewElement)
                TempXML.Save(GetXMLFolder() & "/SERVICE.xml")
                TempXML = Nothing
            End If
            dt.Dispose()
        Catch ex As IO.IOException
        Catch ex As Exception
            txtErr.Text &= ex.Message & vbCrLf & ex.StackTrace
        Finally
            TempXML = Nothing
            NewElement = Nothing
        End Try
    End Sub

    Sub GenerateSummary()
        Dim TempXML As New XDocument
        Dim NewElement As New XElement("NewDataSet")
        Dim SummaryElement As New XElement("TOTAL")
        Dim strSql As String = ""
        Try
            strSql = "Select Count(u.id) user_qty "
            strSql += " From TB_USER u "
            strSql += " inner join TB_COUNTER c on c.id=u.counter_id"
            strSql += " Where isnull(u.counter_id,'0')>'0'"
            strSql += " and c.back_office='0' and c.counter_manager='0'"
            Dim dt As New DataTable
            dt = Engine.Common.ShopConnectDBENG.ExecuteTable(strSql, INIFName, "MainDisplayFlash", "Form1_GenerateSummary")
            If dt.Rows.Count > 0 Then
                SummaryElement.Add(New XElement("total_emp", Convert.ToInt64(dt.Rows(0)("user_qty"))))
            End If
            dt.Dispose()

            strSql = "select distinct queue_no "
            strSql += " from TB_COUNTER_QUEUE "
            strSql += " where convert(varchar(8),service_date,112) = convert(varchar(8),GETDATE(),112) and status='3'"
            Dim dt2 As New DataTable
            dt2 = Engine.Common.ShopConnectDBENG.ExecuteTable(strSql, INIFName, "MainDisplayFlash", "Form1_GenerateSummary")
            SummaryElement.Add(New XElement("total_cus", dt2.Rows.Count))
            NewElement.Add(SummaryElement)
            NewElement.Add(New XElement("LastUpdateOn", LastUpdateOn.ToString("dd/MM/yyyy HH:mm", New Globalization.CultureInfo("en-US"))))

            dt2.Dispose()
            TempXML.Add(NewElement)
            TempXML.Save(GetXMLFolder() & "/SUMMARY.xml")
            TempXML = Nothing
        Catch ex As IO.IOException
        Catch ex As Exception
            txtErr.Text &= ex.Message & vbCrLf & ex.StackTrace
        Finally
            TempXML = Nothing
            NewElement = Nothing
            SummaryElement = Nothing
        End Try

    End Sub

    Dim LastUpdateOn As DateTime = DateTime.Now
    Dim RefreshTime As Integer = 10
    Dim CurrRefresh As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If DateAdd(DateInterval.Minute, RefreshTime, LastUpdateOn) < DateTime.Now Then
            LastUpdateOn = DateTime.Now
            Timer1.Enabled = False
            GeneratePieChart()
            GenerateSummary()
            GenerateService()
            Timer1.Interval = 60000   'ให้ Timer มีการ Refresh ทุก 1 นาที
            Timer1.Enabled = True
            RefreshTime = Convert.ToInt16(Engine.Common.ShopConnectDBENG.GetShopConfig(INIFName, "DashboardRefreshMinute", "Dashboard", "Form1_Timer1.Tick"))

            GC.Collect()
            GC.WaitForFullGCComplete()
            CurrRefresh += 1
            If CurrRefresh >= RefreshTime Then
                CaptureToBackground()

                'Get the list of processes
                Dim myProcess As Process = Process.GetProcessById(lblFlashProcessID.Text)
                myProcess.Kill()

                StartDashboard()
            End If
        End If
    End Sub

    Private Sub CaptureToBackground()
        Try
            Dim gr As Graphics
            Dim bm As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, Drawing.Imaging.PixelFormat.Format32bppArgb)
            gr = Graphics.FromImage(bm)
            gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))

            Dim CaptureFile As String = Application.StartupPath & "\CaptureDashboard.jpg"
            If IO.File.Exists(CaptureFile) = True Then
                IO.File.Delete(CaptureFile)
            End If
            bm.Save(CaptureFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            bm.Dispose()
            gr.Dispose()

            SetDesktopBackground(20, 0&, CaptureFile, &H2 Or &H1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        'Get the list of processes
        Dim myProcess As Process = Process.GetProcessById(lblFlashProcessID.Text)
        myProcess.Kill()

        Dim frm As New frmConfig
        Timer1.Enabled = False
        frm.ShowDialog()
        Button1.Text = "OK"
        If frm.DialogResult = Windows.Forms.DialogResult.Yes Then
            RefreshTime = Convert.ToInt16(Engine.Common.ShopConnectDBENG.GetShopConfig(INIFName, "DashboardRefreshMinute", "Dashboard", "Form1_btnConfig.Click"))
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub


    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Function GetXMLFolder() As String
        Dim XMLFolder As String = Application.StartupPath & "\XML"
        Return XMLFolder
    End Function

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "Dashboard V " & getMyVersion()
        StartDashboard()
        HideDesktopIcon()
        MinimizeAllWindows()
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function
End Class

