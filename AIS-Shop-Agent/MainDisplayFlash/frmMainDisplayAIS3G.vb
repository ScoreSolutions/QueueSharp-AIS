Imports System.IO
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Xml
Imports MainDisplayFlash.Org.Mentalis.Files
Public Class frmMainDisplayAIS3G
    Dim sContentsHead As String
    Dim sContentsFooter As String = ""
    Dim sContents As String

    'Mass
    Dim sErr As String
    Dim bAns As Boolean
    Dim pLoopInterval As Integer
    Dim pLoopCount As Integer
    Dim totalRec As Integer
    Dim Looprec As Integer
    Dim endRow As Integer

    Dim sqlstr As String
    Dim pProcessIdMass As Integer
    Dim ProcessKill As Process
    Dim ConfigXML As New XMLclss
    Dim XMLFileMass As String
    Dim MaxRow As Integer

    Dim flashFilename As String
    Dim flashHeight As String
    Dim flashWidth As String
    Dim flashPosX As String
    Dim flashPosY As String

    Dim SourceTextMassPath As String
    Dim LaunchApp As String
    Dim MainFlashSWF As String



    'Serenade
    Dim sSerenadeHead As String
    Dim sSerenadeContents As String
    Dim sSerenadeFooter As String
    Dim startRowSerenade As Integer
    Dim endRowSerenade As Integer

    Dim flashHeightSerenade As String
    Dim flashWidthSerenade As String
    Dim flashPosXSerenade As String
    Dim flashPosYSerenade As String

    Dim SourceTextSerenadePath As String
    Dim LaunchSerenadeApp As String
    Dim FourSerenadeSWF As String
    Dim FiveSerenadeSWF As String
    Dim SixSerenadeSWF As String
    Dim SevenSerenadeSWF As String
    Dim EightSerenadeSWF As String

    Dim pNewServiceSerenade As Integer
    Dim pOldServiceSerenade As Integer

    Dim FlagSerenadePlay As Boolean = False
    Dim pProcessIdSerenade As Integer
    Dim XMLFileSerenade As String

    'Serenade Club
    Dim pProcessIdSerenadeClub As Integer
    Dim SerenadeClubFlashFilename As String
    Dim SerenadeClubFlashHeight As String
    Dim SerenadeClubFlashWidth As String
    Dim SerenadeClubFlashPosX As String
    Dim SerenadeClubFlashPosY As String

    Dim SerenadeClubSourceTextPath As String
    Dim SerenadeClubLaunchApp As String
    Dim SerenadeClubFlashSWF As String
    Dim SerenadeClubXMLFile As String
    Dim SerenadeClubMaxRow As Integer

    Dim HeartbeatPath As String

    Private Inireader As New IniReader(Application.StartupPath & "\Maindisplay.ini")

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        startRowSerenade = 1
    End Sub
    Public Function GetFileContents(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
            Return ErrInfo
        End Try
    End Function

    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath, False, System.Text.Encoding.UTF8, strData.Length)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return bAns
    End Function

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click
        Timer1.Interval = (My.Settings.Interval * 1000) + 1
        Timer1.Enabled = True
    End Sub

    Private Sub SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Timer1.Enabled = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Public Property ProcessIdMass() As Integer
        Get
            Return pProcessIdMass
        End Get
        Set(ByVal value As Integer)
            pProcessIdMass = value
        End Set
    End Property

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.Text = "Main Display V " & getMyVersion()

        Dim IsConnect As Boolean = False
        Do
            IsConnect = Engine.Common.ShopConnectDBENG.CheckDBConn(Inireader.Filename)
        Loop Until IsConnect = True

        Try
            Inireader.Section = "FlashFileMass"
            If Inireader.ReadString("UseMass") = "Y" Then
                SourceTextMassPath = Application.StartupPath & "\" & Inireader.ReadString("SourceText")
                LaunchApp = Application.StartupPath & "\" & Inireader.ReadString("LaunchApp")
                XMLFileMass = Application.StartupPath & "\" & Inireader.ReadString("XMLConfig")
                MainFlashSWF = Application.StartupPath & "\" & Inireader.ReadString("MainFlash")
                MaxRow = Inireader.ReadString("MaxRow")

                flashHeight = ConfigXML.GetReadXML(XMLFileMass, "flashHeight")
                flashWidth = ConfigXML.GetReadXML(XMLFileMass, "flashWidth")
                flashPosX = ConfigXML.GetReadXML(XMLFileMass, "flashPosX")
                flashPosY = ConfigXML.GetReadXML(XMLFileMass, "flashPosY")
                Timer1.Enabled = True
            End If

            Inireader.Section = "FlashFileSerenade"
            If Inireader.ReadString("UseSerenade") = "Y" Then
                SourceTextSerenadePath = Application.StartupPath & "\" & Inireader.ReadString("SourceText")
                LaunchSerenadeApp = Application.StartupPath & "\" & Inireader.ReadString("LaunchApp")
                XMLFileSerenade = Application.StartupPath & "\" & Inireader.ReadString("XMLConfig")
                FourSerenadeSWF = Application.StartupPath & "\" & Inireader.ReadString("4ServiceFlash")
                FiveSerenadeSWF = Application.StartupPath & "\" & Inireader.ReadString("5ServiceFlash")
                SixSerenadeSWF = Application.StartupPath & "\" & Inireader.ReadString("6ServiceFlash")
                SevenSerenadeSWF = Application.StartupPath & "\" & Inireader.ReadString("7ServiceFlash")
                EightSerenadeSWF = Application.StartupPath & "\" & Inireader.ReadString("8ServiceFlash")

                flashHeightSerenade = ConfigXML.GetReadXML(XMLFileSerenade, "flashHeight")
                flashWidthSerenade = ConfigXML.GetReadXML(XMLFileSerenade, "flashWidth")
                flashPosXSerenade = ConfigXML.GetReadXML(XMLFileSerenade, "flashPosX")
                flashPosYSerenade = ConfigXML.GetReadXML(XMLFileSerenade, "flashPosY")
                Timer2.Enabled = True
            End If

            Inireader.Section = "FlashFileSerenadeClub"
            If Inireader.ReadString("UseSerenadeClub") = "Y" Then
                SerenadeClubSourceTextPath = Application.StartupPath & "\" & Inireader.ReadString("SourceText")
                SerenadeClubLaunchApp = Application.StartupPath & "\" & Inireader.ReadString("LaunchApp")
                SerenadeClubXMLFile = Application.StartupPath & "\" & Inireader.ReadString("XMLConfig")
                SerenadeClubFlashSWF = Application.StartupPath & "\" & Inireader.ReadString("MainFlash")
                SerenadeClubMaxRow = Inireader.ReadString("MaxRow")

                SerenadeClubFlashHeight = ConfigXML.GetReadXML(SerenadeClubXMLFile, "flashHeight")
                SerenadeClubFlashWidth = ConfigXML.GetReadXML(SerenadeClubXMLFile, "flashWidth")
                SerenadeClubFlashPosX = ConfigXML.GetReadXML(SerenadeClubXMLFile, "flashPosX")
                SerenadeClubFlashPosY = ConfigXML.GetReadXML(SerenadeClubXMLFile, "flashPosY")
                Timer3.Enabled = True
            End If

            Inireader.Section = "Heartbeat"
            HeartbeatPath = Inireader.ReadString("path")

            If Not System.IO.Directory.Exists(HeartbeatPath) Then
                System.IO.Directory.CreateDirectory(HeartbeatPath.Substring(0, HeartbeatPath.LastIndexOf("\")))
            End If
            If Not System.IO.File.Exists(HeartbeatPath) Then
                System.IO.File.Create(HeartbeatPath)
            End If

            Try
                Dim i As Integer = LaunchApp.LastIndexOf("\")
                Dim l As Integer = LaunchApp.Length
                Dim Mass As String
                l = l - i
                Mass = LaunchApp.Substring((LaunchApp.LastIndexOf("\")) + 1, l - 5)
                Console.WriteLine(LaunchApp.Substring((LaunchApp.LastIndexOf("\")) + 1, l - 5))
                Console.WriteLine()

                For Each Proc In Process.GetProcessesByName(Mass)
                    Proc.Kill()
                Next
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        Timer1.Enabled = False
    End Sub

    Public Sub New()
        'DuplicateCurrentApp()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Function GetFileinFolder(ByVal Path As String) As List(Of String)
        Try
            Dim fileList As New List(Of String)
            ' make a reference to a directory
            Dim di As New IO.DirectoryInfo(Path)
            Dim diar1 As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            'list the names of all files in the specified directory
            For Each dra In diar1
                fileList.Add(dra.ToString)
            Next
            Return fileList
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        Try
            Dim dt As New DataTable
            dt = Engine.Common.ShopConnectDBENG.ExecuteTable("exec SP_MainDisplaySerenadeClub", Inireader.Filename, "MainDisplayFlash", "frmMainDisplayAIS3G_Timer3_Tick")
            If dt.Rows.Count < SerenadeClubMaxRow Then
                Dim diffRow As Integer = SerenadeClubMaxRow - dt.Rows.Count
                For a As Integer = 1 To diffRow
                    Dim dr As DataRow = dt.NewRow
                    dr("queue_no") = DBNull.Value
                    dr("item_name_th") = DBNull.Value
                    dr("item_name") = DBNull.Value
                    dr("counter_code") = DBNull.Value
                    dt.Rows.Add(dr)
                Next
            End If

            If pProcessIdSerenadeClub = 0 Then
                ConfigXML.SetWriteXML(SerenadeClubXMLFile, SerenadeClubFlashSWF, SerenadeClubFlashHeight, SerenadeClubFlashWidth, SerenadeClubFlashPosX, SerenadeClubFlashPosY)
                pProcessIdSerenadeClub = Shell(SerenadeClubLaunchApp, AppWinStyle.MaximizedFocus)
            End If

            Dim TextHead As String = "CurrentDate=" & Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & "&CurrentTime=" & Now.ToString("HH:mm")
            Dim TextService As String = ""
            Dim TextQueue As String = ""
            For i As Integer = 0 To SerenadeClubMaxRow - 1
                Dim j As Integer = i + 1
                Dim vItemNameTh As String = " "
                Dim vItemNameEn As String = " "
                Dim vQueue As String = " "
                Dim vCounterCode As String = " "
                Dim vTime As String = "0"

                If Convert.IsDBNull(dt.Rows(i)("item_name_th")) = False Then vItemNameTh = dt.Rows(i)("item_name_th")
                If Convert.IsDBNull(dt.Rows(i)("item_name")) = False Then vItemNameEn = "(" & dt.Rows(i)("item_name") & ")"
                If Convert.IsDBNull(dt.Rows(i)("queue_no")) = False Then vQueue = dt.Rows(i)("queue_no")
                If Convert.IsDBNull(dt.Rows(i)("counter_code")) = False Then vCounterCode = dt.Rows(i)("counter_code")
                If Convert.IsDBNull(dt.Rows(i)("atime")) = False Then
                    vTime = GetTimeFormat(dt.Rows(i)("atime"))
                Else
                    vTime = " "
                End If

                TextService += "&TextService_th_" & j & "=" & vItemNameTh & "&TextService_en_" & j & "=" & vItemNameEn & ""
                TextQueue += "&Q" & j & "_1=" & vQueue & "&C" & j & "_1=" & vCounterCode & "&QTime" & j & "_5=" & vTime
            Next

            SaveTextToFile(TextHead & TextService & TextQueue, SerenadeClubSourceTextPath, sErr)
            dt.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")

        Try
            Dim dt As New DataTable
            dt = Engine.Common.ShopConnectDBENG.ExecuteTable("exec SP_MainDisplayNew3G", Inireader.Filename, "MainDisplayFlash", "frmMainDisplayAIS3G_Timer1.Tick")
            If dt.Rows.Count < MaxRow Then
                Dim diffRow As Integer = MaxRow - dt.Rows.Count
                For a As Integer = 1 To diffRow
                    Dim dr As DataRow = dt.NewRow
                    dr("queue_no") = DBNull.Value
                    dr("counter_code") = DBNull.Value
                    dt.Rows.Add(dr)
                Next
            End If

            If ProcessIdMass = 0 Then
                ConfigXML.SetWriteXML(XMLFileMass, MainFlashSWF, flashHeight, flashWidth, flashPosX, flashPosY)
                ProcessIdMass = Shell(LaunchApp, AppWinStyle.MaximizedFocus)
            End If

            sContentsHead = ""
            sContents = ""
            endRow = dt.Rows.Count
            If endRow > MaxRow Then
                endRow = MaxRow
            End If
            Dim j As Integer = 1
            Dim i As Integer = 1
            sContentsHead = "CurrentDate=" & Now.ToString("dd/MM/yyyy") & "&CurrentTime=" & Now.ToString("HH:mm")
            sContents = ""
            For m As Integer = 1 To endRow
                If m <= endRow Then
                    Dim QueueNo As String = " "
                    Dim aTime As String = " "
                    Dim CounterCode As String = " "
                    Dim vStatus As String = ""

                    If Convert.IsDBNull(dt.Rows(m - 1).Item("queue_no")) = False Then
                        QueueNo = dt.Rows(m - 1).Item("queue_no")
                    End If
                    If Convert.IsDBNull(dt.Rows(m - 1).Item("counter_code")) = False Then
                        CounterCode = dt.Rows(m - 1).Item("counter_code")
                    End If
                    If Convert.IsDBNull(dt.Rows(m - 1).Item("status")) = False Then
                        vStatus = dt.Rows(m - 1).Item("status")
                    End If
                    If Convert.IsDBNull(dt.Rows(m - 1).Item("aTime")) = False Then
                        If vStatus = "2" Then
                            aTime = GetTimeFormat(dt.Rows(m - 1).Item("aTime"))
                        End If
                    Else
                        If vStatus = "4" Then
                            aTime = " "
                        End If
                    End If

                    sContents = sContents & "&Q" & m & "_" & m & "=" & QueueNo & "&C" & m & "_s" & m & "=" & CounterCode & "&T" & m & "_" & m & "=" & aTime
                End If
            Next

            sContents = sContentsHead & sContents & sContentsFooter
            bAns = SaveTextToFile(sContents, SourceTextMassPath, sErr)
            dt.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While True
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            Try
                UpdateHB(HeartbeatPath, My.Application.Info.ProductName)
            Catch ex As Exception

            End Try

            System.Threading.Thread.Sleep(5000)
        End While
    End Sub

    Private Function GetTimeFormat(ByVal TimeSec As Long) As String
        'แปลงเวลาจากวินาทีไปเป็น mm:ss
        Dim tMin As Integer = 0
        Dim tSec As Integer = 0

        tMin = Math.Floor(TimeSec / 60)
        tSec = TimeSec Mod 60
        Return "00:" & tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
    End Function

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        Try
            Dim dt As New DataTable
            dt = Engine.Common.ShopConnectDBENG.ExecuteTable("exec SP_MainDisplaySerenade", Inireader.Filename, "MainDisplayFlash", "frmMainDisplayAIS3G_Timer2.Tick")
            If dt.Rows.Count > 0 Then
                dt.TableName = "tb_MainDisplay"
                pNewServiceSerenade = dt.Rows.Count

                If pOldServiceSerenade <> pNewServiceSerenade Then
                    FlagSerenadePlay = False
                    pOldServiceSerenade = pNewServiceSerenade
                End If
                If FlagSerenadePlay = False Then

                    If pProcessIdSerenade <> 0 Then
                        ProcessKill = System.Diagnostics.Process.GetProcessById(pProcessIdSerenade)
                        ProcessKill.Kill()
                    End If

                    Select Case pNewServiceSerenade
                        Case 4
                            ConfigXML.SetWriteXML(XMLFileSerenade, FourSerenadeSWF, flashHeightSerenade, flashWidthSerenade, flashPosXSerenade, flashPosYSerenade)
                            pProcessIdSerenade = Shell(LaunchSerenadeApp, AppWinStyle.MaximizedFocus)
                        Case 5
                            ConfigXML.SetWriteXML(XMLFileSerenade, FiveSerenadeSWF, flashHeightSerenade, flashWidthSerenade, flashPosXSerenade, flashPosYSerenade)
                            pProcessIdSerenade = Shell(LaunchSerenadeApp, AppWinStyle.MaximizedFocus)
                        Case 6
                            ConfigXML.SetWriteXML(XMLFileSerenade, SixSerenadeSWF, flashHeightSerenade, flashWidthSerenade, flashPosXSerenade, flashPosYSerenade)
                            pProcessIdSerenade = Shell(LaunchSerenadeApp, AppWinStyle.MaximizedFocus)
                        Case 7
                            ConfigXML.SetWriteXML(XMLFileSerenade, SevenSerenadeSWF, flashHeightSerenade, flashWidthSerenade, flashPosXSerenade, flashPosYSerenade)
                            pProcessIdSerenade = Shell(LaunchSerenadeApp, AppWinStyle.MaximizedFocus)
                        Case 8
                            ConfigXML.SetWriteXML(XMLFileSerenade, EightSerenadeSWF, flashHeightSerenade, flashWidthSerenade, flashPosXSerenade, flashPosYSerenade)
                            pProcessIdSerenade = Shell(LaunchSerenadeApp, AppWinStyle.MaximizedFocus)
                        Case Else

                    End Select
                    FlagSerenadePlay = True
                End If

                sSerenadeHead = ""
                sSerenadeContents = ""

                endRowSerenade = startRowSerenade + dt.Rows.Count
                Dim j As Integer = 1
                Dim i As Integer = 1
                sSerenadeHead = ""
                sSerenadeHead = "CurrentDate=" & Now.ToString("dd/MM/yyyy") & "&CurrentTime=" & Now.ToString("HH:mm")
                For i = startRowSerenade To endRowSerenade
                    If i < endRowSerenade Then
                        sSerenadeHead = sSerenadeHead & "&TextService_th" & j & "=" & dt.Rows(i - 1).Item("item_name_th") & "&TextService_en" & j & "=" & dt.Rows(i - 1).Item("item_name_en")
                    End If
                    j = j + 1
                Next

                sSerenadeContents = ""
                Dim k As Integer = 1
                Dim n As Integer = 1
                For n = startRowSerenade To endRowSerenade
                    Dim QueueNo As String = " "
                    Dim CounterCode As String = " "
                    If n < endRowSerenade Then
                        QueueNo = " " & dt.Rows(n - 1).Item("queue")
                        CounterCode = " " & dt.Rows(n - 1).Item("counter")
                        If sSerenadeContents = "" Then
                            sSerenadeContents = "&Q" & k & "_" & 1 & "=" & QueueNo & "&C" & k & "_" & 1 & "=" & CounterCode
                        Else
                            sSerenadeContents = sSerenadeContents & "&Q" & k & "_" & 1 & "=" & QueueNo & "&C" & k & "_" & 1 & "=" & CounterCode
                        End If
                        k = k + 1
                    End If

                    'If dt.Rows(i - 1).Item("row") = 4 Then
                    '    k = k + 1
                    'End If
                Next
                'sContentsFooter = "&Queue1_5=&QTime1_5=&Queue2_5=&QTime2_5=&Queue3_5=&QTime3_5=&Queue4_5=&QTime4_5="
                sSerenadeContents = sSerenadeHead & sSerenadeContents
                bAns = SaveTextToFile(sSerenadeContents, SourceTextSerenadePath, sErr)
                startRowSerenade = endRowSerenade
                endRowSerenade = endRowSerenade + dt.Rows.Count
                If startRowSerenade >= dt.Rows.Count Then
                    startRowSerenade = 1
                End If
                dt.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class