Imports System.IO
Imports System.Management
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Collections
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports AMS
Imports DirectX.Capture
Imports System.Threading
Imports System.Globalization
Imports Microsoft.Win32


Public Class QM
    Dim QNo As String = File.ReadAllText(Application.StartupPath & "/TextQueueNo.txt")
    Public param As String
    Dim vSourcePathFile As String
    Dim vDestPathFile As String
    Dim vAgentVideoFile As String
    Dim vCustomerVideoFile As String
    Dim AgentCam As New HostWebcam
    Dim CustomerCam As New GuestWebcam

    Dim Stopwatch As New Stopwatch
    Dim arrInput As New ArrayList
    'Dim ConfigQM As New Profile.Ini("D:\Scoresolutions\ConfigQM.ini")

    Dim RegistryCurrent As RegistryKey
    Dim NotifyAnimate As Boolean = False


    Dim AgentCueFlag As Boolean = False
    Dim AgentCaptureFlag As Boolean = False
    Dim AgentInputDevice As Integer

    Dim CustomerInputDevice As Integer
    Dim CustomerCueFlag As Boolean = False
    Dim CustomerCaptureFlag As Boolean = False

    Dim VdoFtp As New FTPVDO
    Dim FtpHost As String
    Dim FtpUser As String
    Dim FtpPassword As String
    Dim FtpLocalfile As String
    Dim FtpDestinationfile As String
    Dim QMSplitFolder As String

    Dim RedIcon As System.Drawing.Icon
    Dim WhiteIten As System.Drawing.Icon

    'Private computer = New Microsoft.VisualBasic.Devices.Computer()
    'Private operatingSystem As String = computer.Info.OSFullName

    Dim sqlstr As String
    Dim i As Integer
    'dddfdf
 
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim Debug As String = "1 " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            RedIcon = Icon.FromHandle(CType(ImageList1.Images.Item(0), Bitmap).GetHicon)
            WhiteIten = Icon.FromHandle(CType(ImageList1.Images.Item(1), Bitmap).GetHicon)
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            'Get Ftp Host Config
            Dim ds As New DataSet
            ds = New DBClass().SqlGet("select config_name, config_value from tb_setting where config_name ='QMSplitFolder'", "TB_SETTING")
            Dim cDt As New DataTable
            cDt = ds.Tables("TB_SETTING")
            ds.Dispose()

            If cDt.Rows.Count = 1 Then
                Dim ini As New Profile.Ini(Application.StartupPath & "\QueueSharp.ini")
                FtpHost = ini.GetValue("QMFtp", "FtpHost")
                FtpUser = ini.GetValue("QMFtp", "FtpUser")
                FtpPassword = Engine.Common.FunctionEng.DeCripPwd(ini.GetValue("QMFtp", "FtpPassword"))

                QMSplitFolder = cDt.Rows(0)("config_value").ToString

                '************remove Option Display Encoding status Window
                Registry.CurrentUser.CreateSubKey("Software\GNU\XviD\display_status")
                RegistryCurrent = Registry.CurrentUser.OpenSubKey("Software\GNU\XviD\", True)

                If RegistryCurrent Is Nothing Then
                    RegistryCurrent = Registry.CurrentUser.OpenSubKey("Software\GNU\XviD\", True)
                    RegistryCurrent.SetValue("display_status", 0)
                Else
                    RegistryCurrent.SetValue("display_status", 0)
                End If

                If Directory.Exists(Application.StartupPath & "\Temp") = False Then
                    Directory.CreateDirectory(Application.StartupPath & "\Temp")
                End If
                If Directory.Exists(Application.StartupPath & "\Backup") = False Then
                    Directory.CreateDirectory(Application.StartupPath & "\Backup")
                End If

                Dim ConfigCamIni As New Profile.Ini(ini.GetValue("Setting", "CamConfig").ToString)
                AgentInputDevice = ConfigCamIni.GetValue("Agent", "AgentCameraInput")
                CustomerInputDevice = ConfigCamIni.GetValue("Customer", "CustomerCameraInput")
                ConfigCamIni = Nothing
                ini = Nothing

                'QNo = "20131011104822-F001"
                If QNo.Trim.Length > 0 Then
                    'QNo = DateTime.Now.ToString("yyyyMMddHHmmss") & "-" & QNo 'Now.ToString("yyyy") & Now.ToString("MM") & Now.ToString("dd") & Now.ToString("HH") & Now.ToString("mm") & Now.ToString("ss") & "-" & QNo
                    If AgentCam.InitializeHostWebcam(AgentInputDevice, QNo) = True And CustomerCam.InitializeGuestWebcam(CustomerInputDevice, QNo) = True Then
                        AgentCueFlag = True
                        CustomerCueFlag = True
                    Else
                        AgentCam.HostDispose()
                        CustomerCam.GuestDispose()
                        Errorlog("Cannot Initialize camera QueueNo:" & QNo)
                        ErrorLogDB("QM Event Form1 :Cannot Initialize camera QueueNo:" & QNo)
                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                        Application.Exit()
                    End If
                    If AgentCueFlag = True And CustomerCueFlag = True Then
                        EventLog(QNo & " Start Record")
                        WriteAVSScript(Application.StartupPath & "\Temp\" & QNo & "AA.avi", Application.StartupPath & "\Temp\" & QNo & "BB.avi", QNo)

                        AgentCam.HostStart()
                        CustomerCam.GuestStart()

                        Me.WindowState = FormWindowState.Minimized
                        Me.Hide()
                    Else
                        Errorlog("Cannot start camera")
                        ErrorLogDB("QM Event Form1 : Cannot Start camera")
                        AgentCam.HostDispose()
                        CustomerCam.GuestDispose()

                        Try
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                        Catch ex As Exception

                        End Try
                        Application.Exit()
                    End If
                Else
                    Errorlog("Queue number empty")
                    ErrorLogDB("QM Event Form1 : Queue number empty")
                    AgentCam.HostDispose()
                    CustomerCam.GuestDispose()
                    Try
                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                    Catch ex As Exception

                    End Try
                    Application.Exit()
                End If
            Else
                Errorlog("Connot get Config QMFtpConfig")
                ErrorLogDB("QM Event Form1 : Connot get Config QMFtpConfig")
                Application.Exit()
            End If
        Catch ex As Exception
            AgentCam.HostDispose()
            CustomerCam.GuestDispose()
            Try
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
            Catch ex1 As Exception

            End Try
            Errorlog("Form1_Load.Exception : " & ex.Message)
            ErrorLogDB("QM Event Form1 : Exception " & ex.Message)
            Application.Exit()
        End Try

    End Sub

    Public Function SaveFile(ByVal Filename As String, ByVal byteData() As Byte) As Boolean
        'Dim byteData() As Byte = Nothing

        ' Create a file and write the byte data to a file.
        Dim oFileStream As System.IO.FileStream
        Try
            oFileStream = New System.IO.FileStream(Filename, System.IO.FileMode.Create)
            oFileStream.Write(byteData, 0, byteData.Length)
            oFileStream.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function FileToByteArray(ByVal _FileName As String) As Byte()
        Dim _Buffer() As Byte = Nothing

        Try
            ' Open file for reading
            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' attach filestream to binary reader
            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)

            ' get total byte length of the file
            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length

            ' read entire file into buffer
            _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))

            ' close file reader
            _FileStream.Close()
            _FileStream.Dispose()
            _BinaryReader.Close()
        Catch _Exception As Exception
            ' Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString())
        End Try

        Return _Buffer
    End Function

    Public Sub New()

    
        TerminateConfigApp()
        'DuplicateCurrentApp()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub DuplicateCurrentApp()
        Dim getProcess() As Process = Process.GetProcessesByName(My.Application.Info.ProductName)
        If getProcess.Length > 1 Then
            End
        End If
    End Sub
    Private Sub TerminateConfigApp()
        Dim getProcess() As Process = Process.GetProcessesByName("ConfigInput")
        For Each p As Process In getProcess
            p.Kill()
        Next
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If NotifyAnimate = False Then
                NotifyIcon1.Icon = RedIcon 'Icon.FromHandle(CType(ImageList1.Images.Item(0), Bitmap).GetHicon)
                NotifyAnimate = True
            Else
                NotifyIcon1.Icon = WhiteIten 'Icon.FromHandle(CType(ImageList1.Images.Item(1), Bitmap).GetHicon)
                NotifyAnimate = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Pause
                If True Then
                    DoStop()
                    Exit Select
                End If
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Public Function GetIPAddress() As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
            oAddr = New System.Net.IPAddress(.AddressList(0).Address)
            sAddr = oAddr.ToString
        End With
        GetIPAddress = sAddr
    End Function
    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
        'Dim company As String = Application.CompanyName
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

            Dim ds As New DataSet
            ds = New DBClass().SqlGet(sql, "TB_COUNTER_QUEUE")
            dt = ds.Tables("TB_COUNTER_QUEUE")
            ds.Dispose()
            'EventLog(sql)
        Catch ex As Exception
            Errorlog("QM_GetQueueDataByDate Exception : " & ex.Message)
        End Try
        
        Return dt
    End Function
  
    Private Sub DoStop()
        Dim Qid As String = File.ReadAllText(Application.StartupPath & "\QM.txt")
        Dim QNo As String = File.ReadAllText(Application.StartupPath & "\TextQueueNo.txt")
        Dim finish As Boolean = False
        Dim FileInBackup As New ArrayList
        Dim sDurationA As String
        Dim sDurationB As String
        Dim tTimespanA As TimeSpan
        Dim tTimespanB As TimeSpan
        Dim tTimespanSubstract As TimeSpan
        Dim tTimespanCrop As TimeSpan
        Dim totalframe As Long
        Dim ds As New DataSet
        Dim DoService As String

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Try
            EventLog("Stop Capture : QueueNo=" & QNo)
            If AgentCam.HostStop() = True And CustomerCam.GuestStop() = True Then
                If Qid.Trim.Length > 0 And QNo.Trim.Length > 0 Then
                    Try
                        System.Threading.Thread.Sleep(2000)
                        Dim AgentInfo As New FileInfo(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                        Dim CustomerInfo As New FileInfo(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                        If AgentInfo.Length > 0 And CustomerInfo.Length > 0 Then
                            EventLog("Start Merge File : QueueNo=" & QNo)
                            UpdateQmTransferLog(QmTransferStatus.FileIsMerging, Qid)

                            sDurationA = AVIReadInfo(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                            sDurationA = (sDurationA.Substring(InStr(sDurationA, "Duration") + 8, 11))
                            tTimespanA = New TimeSpan(0, sDurationA.Substring(1, 2), sDurationA.Substring(4, 2), sDurationA.Substring(7, 2), sDurationA.Substring(10, 1))

                            sDurationB = AVIReadInfo(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                            sDurationB = (sDurationB.Substring(InStr(sDurationB, "Duration") + 8, 11))
                            tTimespanB = New TimeSpan(0, sDurationB.Substring(1, 2), sDurationB.Substring(4, 2), sDurationB.Substring(7, 2), sDurationB.Substring(10, 1))

                            If tTimespanA.Duration > tTimespanB.Duration Then
                                ExtractSound(Application.StartupPath & "\Temp\" & QNo & "B.avi", Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                tTimespanSubstract = tTimespanB
                                If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & ".mp3") Then
                                    Errorlog("No Sound File For " & Qid)
                                    ErrorLogDB("QM_DoStop : No Sound File For " & Qid)
                                End If
                            Else
                                ExtractSound(Application.StartupPath & "\Temp\" & QNo & "A.avi", Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                tTimespanSubstract = tTimespanA
                                If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & ".mp3") Then
                                    Errorlog("No Sound File For " & Qid)
                                    ErrorLogDB("QM_DoStop : No Sound File For " & Qid)
                                End If
                            End If

                            totalframe = tTimespanSubstract.TotalSeconds * 15

                            Dim ChkQty As Integer = 0
                            While Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                ATrimAVSscript(Application.StartupPath & "\Temp\" & QNo & "A.avi", totalframe, QNo)
                                AVSTrimA(Application.StartupPath & "\Temp\" & QNo & "AA.avi", QNo)
                                If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "AA.avi") Then
                                    ChkQty += 1
                                    If ChkQty >= 5 Then
                                        Errorlog("No Vdo AA  File For " & Qid)
                                        ErrorLogDB("QM_DoStop : No Vdo AA  File For " & Qid)
                                        Exit While
                                    End If
                                End If
                            End While

                            ChkQty = 0
                            While Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                BTrimAVSscript(Application.StartupPath & "\Temp\" & QNo & "B.avi", totalframe, QNo)
                                AVSTrimB(Application.StartupPath & "\Temp\" & QNo & "BB.avi", QNo)
                                If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "BB.avi") Then
                                    ChkQty += 1
                                    If ChkQty >= 5 Then
                                        Errorlog("No Vdo BB  File For " & Qid)
                                        ErrorLogDB("QM_DoStop : No Vdo BB  File For " & Qid)
                                        Exit While
                                    End If
                                End If
                            End While

                            ChkQty = 0
                            While Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                MakeSplitScreen(Application.StartupPath & "\Temp\" & QNo & "ABC.avi", QNo)
                                If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & "ABC.avi") Then
                                    ChkQty += 1
                                    If ChkQty >= 5 Then
                                        Errorlog("No Vdo ABC File For " & Qid)
                                        ErrorLogDB("QM_DoStop : No Vdo ABC  File For " & Qid)
                                        Exit While
                                    End If
                                End If
                            End While

                            tTimespanCrop = TimeSpan.FromSeconds(1)
                            tTimespanSubstract = tTimespanSubstract.Subtract(tTimespanCrop)
                            If File.Exists(Application.StartupPath & "\Temp\" & QNo & ".mp3") Then
                                ChkQty = 0
                                While Not File.Exists(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                    MixSoundAndVideo(Application.StartupPath & "\Temp\" & QNo & ".mp3", Application.StartupPath & "\Temp\" & QNo & "ABC.avi", Application.StartupPath & "\Temp\" & QNo & ".flv")
                                    If Not File.Exists(Application.StartupPath & "\Temp\" & QNo & ".flv") Then
                                        ChkQty += 1
                                        If ChkQty >= 5 Then
                                            Errorlog("No FLV File For " & Qid)
                                            ErrorLogDB("QM_DoStop : No FLV File For " & Qid)
                                            UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)
                                            Exit While
                                        End If
                                    End If
                                End While
                                CropVideo(tTimespanSubstract.Duration.ToString, Application.StartupPath & "\Temp\" & QNo & ".flv", Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                            Else
                                Errorlog("No sound capture")
                                ErrorLogDB("QM_DoStop : No sound capture")
                                ConvertFLVNoSound(Application.StartupPath & "\Temp\" & QNo & "ABC.avi", Application.StartupPath & "\Temp\" & QNo & ".flv")
                                CropVideo(tTimespanSubstract.Duration.ToString, Application.StartupPath & "\Temp\" & QNo & ".flv", Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                            End If

                            If File.Exists(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv") Then
                                UpdateQmTransferLog(QmTransferStatus.FileIsAlreadyMerged, Qid)
                                Dim FolderName As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                FtpLocalfile = Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv"

                                If QMSplitFolder = "Y" Then
                                    FtpDestinationfile = "/" & FolderName & "/" & Now.ToString("yy") & Now.ToString("MM") & Now.ToString("dd") & Qid & ".flv"
                                Else
                                    FtpDestinationfile = "/" & Now.ToString("yy") & Now.ToString("MM") & Now.ToString("dd") & Qid & ".flv"
                                End If

                                UpdateQmTransferLog(QmTransferStatus.FileIsTransferringFromTempFolder, Qid)
                                finish = VdoFtp.WriteTempFTP(FtpHost, FtpUser, FtpPassword, FtpLocalfile, FtpDestinationfile, FolderName, QMSplitFolder)
                                If finish = True Then
                                    EventLog("FTPtranferVDO Qid " & Qid)
                                    If UpdateQmTransferLog(QmTransferStatus.TransferComplete, Qid) = True Then
                                        Try
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                                        Catch ex As Exception
                                            Errorlog("1.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                                            ErrorLogDB("QM_DoStop : 1.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                                        End Try
                                    Else
                                        'Cannot Update Log status to Complete
                                        Errorlog("1.1 Cannot Update Log status to Complete QueueNo=" & Qid)
                                        ErrorLogDB("QM_DoStop : 1.1 Cannot Update Log status to Complete QueueNo=" & Qid)

                                        UpdateQmTransferLog(QmTransferStatus.FileMovingToBackupFolder, Qid)
                                        Try
                                            File.Copy(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv", Application.StartupPath & "\Backup\" & Now.ToString("yy") & Now.ToString("MM") & Now.ToString("dd") & Qid & ".flv")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                                            File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                                        Catch ex As Exception
                                            Errorlog("1.2 Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                                            ErrorLogDB("QM_DoStop : 1.2 Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                                        End Try
                                    End If
                                    Application.Exit()
                                Else
                                    Errorlog("Cannot Transfer Vdo to Ftp Display Server QueueNo=" & Qid)
                                    ErrorLogDB("QM_DoStop : Cannot Transfer Vdo to Ftp Display Server QueueNo=" & Qid)

                                    UpdateQmTransferLog(QmTransferStatus.FileMovingToBackupFolder, Qid)
                                    Try
                                        File.Copy(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv", Application.StartupPath & "\Backup\" & Now.ToString("yy") & Now.ToString("MM") & Now.ToString("dd") & Qid & ".flv")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                                        File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                                    Catch ex As Exception
                                        Errorlog("2.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                                        ErrorLogDB("QM_DoStop : 2.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                                    End Try
                                End If
                                AgentCam.HostStop()
                                CustomerCam.GuestStop()
                                Application.Exit()
                            Else
                                Errorlog("No video file from encoding QueueNo=" & Qid)
                                ErrorLogDB("QM_DoStop : No video file from encoding QueueNo=" & Qid)
                                UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)

                                Try
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                                    File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                                Catch ex As Exception
                                    Errorlog("3.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                                    ErrorLogDB("QM_DoStop : 3.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                                End Try
                            End If
                        Else
                            'UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)

                            If AgentInfo.Length < 2 Then
                                Errorlog("No output file from agent camera for " & DoService & " Service. QueueNo=" & Qid)
                                ErrorLogDB("QM_DoStop : No output file from agent camera for " & DoService & " Service. QueueNo=" & Qid)
                            ElseIf CustomerInfo.Length < 2 Then
                                Errorlog("No output file from customer camera for " & DoService & " Service QueueNo=" & Qid)
                                ErrorLogDB("QM_DoStop : No output file from customer camera for " & DoService & " Service QueueNo=" & Qid)
                            Else
                                Errorlog("Video capture fail")
                                ErrorLogDB("QM_DoStop : Video capture fail QueueNo=" & QNo)
                            End If

                            Try
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                                File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                            Catch ex As Exception
                                Errorlog("4.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                                ErrorLogDB("QM_DoStop : 4.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                            AgentCam.HostStop()
                            CustomerCam.GuestStop()
                            Application.Exit()
                        End If
                    Catch ex As Exception
                        Errorlog("4.1 " & ex.Message)
                        ErrorLogDB("QM_DoStop : Exception : " & ex.Message & " QueueNo=" & QNo)

                        UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)

                        Try
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo.Substring(15) & Qid & ".flv")
                        Catch ex1 As Exception
                            Errorlog("5.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex1.Message)
                            ErrorLogDB("QM_DoStop : 5.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex1.Message & vbNewLine & ex1.StackTrace)
                        End Try

                        AgentCam.HostStop()
                        CustomerCam.GuestStop()
                        Application.Exit()
                    End Try
                Else
                    'UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)
                    If Qid.Trim.Length = 0 And QNo.Trim.Length > 0 Then
                        EventLog("Delete All File QueueNo:" & QNo)

                        Try
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
                            File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                        Catch ex As Exception
                            Errorlog("6.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message)
                            ErrorLogDB("QM_DoStop : 6.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex.Message & vbNewLine & ex.StackTrace)
                        End Try
                        AgentCam.HostStop()
                        CustomerCam.GuestStop()
                        Application.Exit()
                    End If
                End If
            Else

                'UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)

            End If
        Catch ex As Exception
            Errorlog("6.1 " & ex.Message & " QueueNo=" & QNo)
            ErrorLogDB("QM_DoStop : Exception : " & ex.Message & " QueueNo=" & QNo)
            UpdateQmTransferLog(QmTransferStatus.FileNotFound, Qid)

            Try
                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".flv")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "A.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "B.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "AA.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "BB.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "ABC.avi")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".mp3")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & ".avs")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
                File.Delete(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
            Catch ex1 As Exception
                Errorlog("7.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex1.Message)
                ErrorLogDB("QM_DoStop : 7.Cannot Delete Temp file QueueNo=" & Qid & " : Exception :" & ex1.Message & vbNewLine & ex1.StackTrace)
            End Try

            AgentCam.HostStop()
            CustomerCam.GuestStop()
            Application.Exit()
        End Try
        AgentCam.HostStop()
        CustomerCam.GuestStop()
        Application.Exit()
    End Sub

    'Private Function InsertQmTransferLog(ByVal vStatus As String, ByVal Qid As String) As Long
    '    Dim ret As Long = 0
    '    Dim dt As New DataTable
    '    dt = GetQueueDataByDate(DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")), Qid)
    '    If dt.Rows.Count = 1 Then
    '        Dim qDr As DataRow = dt.Rows(0)
    '        Dim ServiceDate As String = Convert.ToDateTime(qDr("service_date")).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))

    '        Dim sql As String = "declare @MaxID bigint;set @MaxID=(select isnull(max(id),0)+1 from TB_QM_TRANSFER_LOG)"
    '        sql += "insert into TB_QM_TRANSFER_LOG (id,create_by,create_date,transfer_date,service_date,tb_counter_queue_id,"
    '        sql += " queue_no,mobile_no,item_id,item_name_en,counter_id,counter_name,status)"
    '        sql += " values(@MaxID,'" & qDr("username") & "',getdate(),getdate(),'" & ServiceDate & "','" & qDr("id") & "',"
    '        sql += " '" & qDr("queue_no") & "','" & qDr("moblie_no") & "','" & qDr("item_id") & "','" & qDr("item_name") & "','" & qDr("counter_id") & "','" & qDr("counter_name") & "','" & vStatus & "')"

    '        ret = New DBClass().SqlExecute(sqlstr)
    '    End If
    '    dt.Dispose()

    '    Return ret
    'End Function
    Private Function UpdateQmTransferLog(ByVal vStatus As String, ByVal Qid As String) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        Dim vUserName As String = ""
        Dim db As New DBClass
        Try

            sql += "select username "
            sql += " from TB_USER "
            sql += " where id = (select USER_ID "
            sql += "            from TB_COUNTER_QUEUE where id='" & Qid & "'"
            sql += "            and DATEDIFF(d,service_date,getdate())=0)"

            Dim ds As DataSet = db.SqlGet(sql, "TB_USER")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    vUserName = ds.Tables(0).Rows(0)("username").ToString
                End If
            End If
            ds.Dispose()
        Catch ex As Exception
            Errorlog(ex.Message & "### SQL=" & sql)
            ErrorLogDB("QM_UpdateQmTransferLog : Exception : " & ex.Message & "### SQL=" & sql)
        End Try
        
        Try
            sql = "update TB_QM_TRANSFER_LOG"
            sql += " set update_date=getdate(), update_by='" & vUserName & "', status='" & vStatus & "'"
            If vStatus = QmTransferStatus.TransferComplete Then
                sql += ",transfer_date=getdate() "
            End If
            sql += " where tb_counter_queue_id='" & Qid & "' and datediff(d,getdate(),service_date)=0"
            ret = (db.SqlExecute(sql) > 0)

            EventLog("Update TB_QM_TRANSFER_LOG.status Qid=" & Qid & " to " & vStatus)
        Catch ex As Exception
            Errorlog(ex.Message & "### SQL=" & sql)
            ErrorLogDB("QM_UpdateQmTransferLog : Exception : " & ex.Message & "### SQL=" & sql)
        End Try
        db = Nothing

        Return ret
    End Function

    Public Sub Errorlog(ByVal ErrMsg As String)
        Dim ErrLogDir As String = Application.StartupPath & "\ErrorLog\" & Now.ToString("yyyyMM")
        If Directory.Exists(Application.StartupPath & "\ErrorLog") = False Then
            Directory.CreateDirectory(Application.StartupPath & "\ErrorLog")
        End If
        If Directory.Exists(ErrLogDir) = False Then
            Directory.CreateDirectory(ErrLogDir)
        End If

        Dim FILE_NAME As String = ErrLogDir & "\ErrorLog_" & DateTime.Now.ToString("yyyyMMdd") & ".log"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & ErrMsg & Chr(13))
        objWriter.Close()
    End Sub
    Public Sub EventLog(ByVal TextMsg As String)
        Dim EvtLogDir As String = Application.StartupPath & "\EventLog\" & Now.ToString("yyyyMM")
        If Directory.Exists(Application.StartupPath & "\EventLog") = False Then
            Directory.CreateDirectory(Application.StartupPath & "\EventLog")
        End If
        If Directory.Exists(EvtLogDir) = False Then
            Directory.CreateDirectory(EvtLogDir)
        End If

        Dim FILE_NAME As String = EvtLogDir & "\EventLog_" & DateTime.Now.ToString("yyyyMMdd") & ".log"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13))
        objWriter.Close()
    End Sub

    Private Sub ErrorLogDB(ByVal errmsg As String)
        sqlstr = "declare @ID as int;select @ID = (select isnull(MAX(id + 1),1) as id from TB_Error_Log);"
        sqlstr = sqlstr & "Insert Into TB_Error_Log (id,client_ip, log_date,error_message,version) Values (@ID,'" & GetIPAddress() & "',GetDate(),'" & errmsg.Replace("'", "''") & "','" & getMyVersion() & "')"
        i = New DBClass().SqlExecute(sqlstr)
    End Sub
    
    Private Function GetFileinFolder(ByVal Path As String) As List(Of String)
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
    End Function
    Private Sub ClearFile()
        For Each f In Directory.GetFiles(Application.StartupPath & "\Temp\", "*.avi", SearchOption.AllDirectories)
            File.Delete(f)
        Next
        For Each f In Directory.GetFiles(Application.StartupPath & "\Temp\", "*.wav", SearchOption.AllDirectories)
            File.Delete(f)
        Next
    End Sub
    Private Sub UnRar(ByVal WorkingDirectory As String, ByVal filepath As String)

        ' Microsoft.Win32 and System.Diagnostics namespaces are imported
        Dim objRegKey As RegistryKey
        objRegKey = Registry.ClassesRoot.OpenSubKey("WinRAR\Shell\Open\Command")
        ' Windows 7 Registry entry for WinRAR Open Command

        Dim obj As Object = objRegKey.GetValue("")

        Dim objRarPath As String = obj.ToString()
        objRarPath = objRarPath.Substring(1, objRarPath.Length - 7)

        objRegKey.Close()

        Dim objArguments As String
        ' in the following format
        ' " X G:\Downloads\samplefile.rar G:\Downloads\sampleextractfolder\"
        objArguments = " X " & " " & filepath & " " + " " + WorkingDirectory

        Dim objStartInfo As New ProcessStartInfo()
        ' Set the UseShellExecute property of StartInfo object to FALSE
        ' Otherwise the we can get the following error message
        ' The Process object must have the UseShellExecute property set to false in order to use environment variables.
        objStartInfo.UseShellExecute = False
        objStartInfo.FileName = objRarPath
        objStartInfo.Arguments = objArguments
        objStartInfo.WindowStyle = ProcessWindowStyle.Hidden
        objStartInfo.WorkingDirectory = WorkingDirectory & "\"

        Dim objProcess As New Process()
        objProcess.StartInfo = objStartInfo
        objProcess.Start()

    End Sub
    Private Function AVIReadInfo(ByVal SourcePath As String) As String
        Dim piTask As New ProcessStartInfo(Application.StartupPath & "\ffmpeg.exe") With {.UseShellExecute = False, .WindowStyle = ProcessWindowStyle.Hidden, .CreateNoWindow = True, .RedirectStandardError = True}
        piTask.Arguments = "-i " & Chr(34) & SourcePath & Chr(34) ' -i flag tells FFMPEG to output file information, Chr(34) is a " ' 
        Dim prcTask As New Process() With {.StartInfo = piTask}
        prcTask.Start()

        prcTask.WaitForExit(-1)

        Dim sr As StreamReader = prcTask.StandardError
        Dim lsOutput As New List(Of String)
        'read the output lines into a list so we can mess about with them 
        'this adds a little overhead but makes working with the FFMPEG output easier. 
        Do Until sr.EndOfStream
            lsOutput.Add(sr.ReadLine())
        Loop
        sr.Close()
        prcTask.Close()

        Dim strOutput() As String = lsOutput.ToArray()
        Dim strToRead(lsOutput.Count) As String
        Dim i As Integer
        For j As Integer = 0 To lsOutput.Count - 1
            strToRead(j) = strOutput(j)
        Next
        For i = 0 To lsOutput.Count - 1
            If InStr(strToRead(i), "Duration") > 0 Then
                i = i
                Exit For
            End If
        Next

        Return strToRead(i)
    End Function

    Private Function AVIReadInfo1(ByVal SourcePath As String) As String
        'On Error Resume Next

        'If File.Exists(Application.StartupPath & "\ffmpeg.exe") = True Then
        '    File.Delete(Application.StartupPath & "\ffmpeg.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\ffmpeg.rar")

        Dim piTask As New ProcessStartInfo(Application.StartupPath & "\ffmpeg.exe") With {.UseShellExecute = False, .WindowStyle = ProcessWindowStyle.Hidden, .CreateNoWindow = True, .RedirectStandardError = True}
        piTask.Arguments = "-i " & Chr(34) & SourcePath & Chr(34) ' -i flag tells FFMPEG to output file information, Chr(34) is a " ' 
        Dim prcTask As New Process() With {.StartInfo = piTask}
        prcTask.Start()

        prcTask.WaitForExit(-1)

        Dim sr As StreamReader = prcTask.StandardError
        Dim lsOutput As New List(Of String)
        'read the output lines into a list so we can mess about with them 
        'this adds a little overhead but makes working with the FFMPEG output easier. 
        Do Until sr.EndOfStream
            lsOutput.Add(sr.ReadLine())
        Loop
        sr.Close()
        prcTask.Close()
        Dim strOutput() As String = lsOutput.ToArray()
        Dim strToRead(10) As String
        Dim i As Integer
        '13 14 15 are the interesting lines. 

        strToRead(0) = strOutput(10)
        strToRead(1) = strOutput(11)
        strToRead(2) = strOutput(12)
        strToRead(3) = strOutput(13)
        strToRead(4) = strOutput(14)
        strToRead(5) = strOutput(15)
        strToRead(6) = strOutput(16)
        strToRead(7) = strOutput(17)
        strToRead(8) = strOutput(18)
        strToRead(9) = strOutput(19)

        For i = 0 To 9
            If InStr(strToRead(i), "Duration") > 0 Then
                i = i
                Exit For
            End If
        Next
        Return strToRead(i)
    End Function

    'Function GetDuration(ByVal SourceFilePath As String) As String
    '    Dim DetailsIndex As Integer
    '    If File.Exists(SourceFilePath) Then
    '        Dim objShell As Object = CreateObject("Shell.Application")
    '        Dim objFolder As Object = objShell.Namespace(Path.GetDirectoryName(SourceFilePath))
    '        For Each strFileName In objFolder.Items

    '            If strFileName.Name = Path.GetFileName(SourceFilePath) Then
    '                If InStr(operatingSystem, "XP") > 0 Then
    '                    DetailsIndex = 21
    '                ElseIf InStr(operatingSystem, "7") > 0 Then
    '                    DetailsIndex = 27

    '                ElseIf InStr(operatingSystem, "Vista") > 0 Then
    '                    DetailsIndex = 36
    '                End If
    '                Return objFolder.GetDetailsOf(strFileName, DetailsIndex).ToString
    '                Exit For
    '                Exit Function
    '            End If
    '        Next
    '        Return ""
    '    Else
    '        Return ""
    '    End If
    'End Function
    Private Sub WriteAVSScript(ByVal FirstVideoPath As String, ByVal SecondVideoPath As String, ByVal QNo As String)
        Dim sw As StreamWriter
        Dim doublequote As String = Chr(34)
        sw = File.CreateText(Application.StartupPath & "\Temp\" & QNo & ".avs")
        sw.WriteLine("a = DirectShowSource(" & doublequote & FirstVideoPath & doublequote & ",fps=15, convertfps=true)")
        sw.WriteLine("b = DirectShowSource(" & doublequote & SecondVideoPath & doublequote & ",fps=15, convertfps=true)")
        sw.WriteLine("StackHorizontal(a, b)")
        'sw.WriteLine("d = Audiodub(a, b)")
        'sw.WriteLine("Return (d)")
        sw.Close()

    End Sub
    Private Sub ATrimAVSscript(ByVal VideoPath As String, ByVal TotalTrimframe As Long, ByVal QNo As String)
        Dim sw As StreamWriter
        Dim doublequote As String = Chr(34)
        sw = File.CreateText(Application.StartupPath & "\Temp\" & QNo & "trima.avs")
        sw.WriteLine("DirectShowSource(" & doublequote & VideoPath & doublequote & ")")
        sw.WriteLine("a = Trim(0," & TotalTrimframe & ",false)")
        sw.WriteLine("Return a")
        sw.Close()
    End Sub
    Private Sub BTrimAVSscript(ByVal VideoPath As String, ByVal TotalTrimframe As Long, ByVal QNo As String)
        Dim sw As StreamWriter
        Dim doublequote As String = Chr(34)
        sw = File.CreateText(Application.StartupPath & "\Temp\" & QNo & "trimb.avs")
        sw.WriteLine("DirectShowSource(" & doublequote & VideoPath & doublequote & ")")
        sw.WriteLine("b = Trim(0," & TotalTrimframe & ",false)")
        sw.WriteLine("Return b")
        sw.Close()
    End Sub
    ''' <summary>
    ''' รวมไฟล์เสียงและภาพโดยมี Output File เป็น FLV
    ''' </summary>
    ''' <param name="InputSoundFile">ตัวอย่างเช่น.C:\XXX.wav</param>
    ''' <param name="InputVideoFile">ตัวอย่างเช่น.C:\XXX.avi</param>
    ''' <param name="OutputFLVFile">ตัวอย่างเช่น.C:\XXX.flv</param>
    ''' <remarks></remarks>
    ''' 
    Private Sub ConvertFLV(ByVal InputSoundFile As String, ByVal InputVideoFile As String, ByVal OutputFLVFile As String)
        'If File.Exists(Application.StartupPath & "\ffmpeg.exe") = True Then
        '    File.Delete(Application.StartupPath & "\ffmpeg.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\ffmpeg.rar")

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\ffmpeg.exe"
        proc.StartInfo.Arguments = "-i " & InputSoundFile & " -i " & InputVideoFile & " -vcodec flv  -r 15 -ar 44100 -ab 22k  -b 100000 -s 640x240 -async 1   " & OutputFLVFile
        proc.Start()
        proc.WaitForExit()
        proc.Close()

    End Sub
    Private Sub CropVideo(ByVal Duration As String, ByVal InputSource As String, ByVal OutputSource As String)
        'If File.Exists(Application.StartupPath & "\ffmpeg.exe") = True Then
        '    File.Delete(Application.StartupPath & "\ffmpeg.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\ffmpeg.rar")

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\ffmpeg.exe"
        proc.StartInfo.Arguments = "-t " & Duration & " -i " & InputSource & " -acodec copy -vcodec copy -async 1 -vsync 1 " & OutputSource
        proc.Start()
        proc.WaitForExit()
        proc.Close()

    End Sub
    Private Sub ExtractSound(ByVal InputSource As String, ByVal OutputSoundFile As String)
        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\ffmpeg.exe"
        'proc.StartInfo.Arguments = "-i " & InputSource & " -vn -acodec copy -f mp3  " & OutputSoundFile
        proc.StartInfo.Arguments = "-i " & InputSource & " -ab 320000 -ar 44100 -f mp3  " & OutputSoundFile
        proc.Start()
        proc.WaitForExit()
        proc.Close()

    End Sub


    Private Sub AVSTrimA(ByVal Destination_file As String, ByVal QNo As String)
        'If File.Exists(Application.StartupPath & "\avs2avi.exe") = True Then
        '    File.Delete(Application.StartupPath & "\avs2avi.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\avs2avi.rar")
        'System.Threading.Thread.Sleep(2000)

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\avs2avi.exe"
        proc.StartInfo.Arguments = Application.StartupPath & "\Temp\" & QNo & "trima.avs " & Destination_file & " -w -c XVID -q"
        proc.Start()
        proc.WaitForExit()
        proc.Close()
    End Sub
    Private Sub AVSTrimB(ByVal Destination_file As String, ByVal QNo As String)
        'If File.Exists(Application.StartupPath & "\avs2avi.exe") = True Then
        '    File.Delete(Application.StartupPath & "\avs2avi.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\avs2avi.rar")
        'System.Threading.Thread.Sleep(2000)

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\avs2avi.exe"
        proc.StartInfo.Arguments = Application.StartupPath & "\Temp\" & QNo & "trimb.avs " & Destination_file & " -w -c XVID -q"
        proc.Start()
        proc.WaitForExit()
        proc.Close()
    End Sub
    Private Sub MakeSplitScreen(ByVal Destination_file As String, ByVal QNo As String)

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\avs2avi.exe"
        proc.StartInfo.Arguments = Application.StartupPath & "\Temp\" & QNo & ".avs " & Destination_file & " -w -c XVID -q"
        proc.Start()
        proc.WaitForExit()
        proc.Close()

    End Sub
    Private Sub MixSoundAndVideo(ByVal InputSound As String, ByVal InputVideo As String, ByVal OutputFile As String)
        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\ffmpeg.exe"
        'proc.StartInfo.Arguments = "-i " & InputSound & " -i " & InputVideo & " -vcodec flv -qscale 4  -r 15 -ar 44100 -ab 22k  -vb 400k -s 640x240 -async 1  " & OutputFile
        proc.StartInfo.Arguments = "-i " & InputSound & " -i " & InputVideo & " -vcodec flv -qscale 5  -ab 32k -ar 44100  -r 15 -b 200000   -s 640x240 -async 1  " & OutputFile
        'ffmpeg -vcodec flv -qscale 9.5 -r 25 -ar 22050 -ab 32k -s 320x240 -i 1.mp3 -i Meta.ogv final.flv
        proc.Start()
        proc.WaitForExit()
        proc.Close()
    End Sub
    ''' <summary>
    ''' รวมไฟล์เสียงและภาพโดยมี Output File เป็น FLV
    ''' </summary>
    ''' <param name="InputSoundFile">ตัวอย่างเช่น.C:\XXX.wav</param>
    ''' <param name="InputVideoFile">ตัวอย่างเช่น.C:\XXX.avi</param>
    ''' <param name="OutputFLVFile">ตัวอย่างเช่น.C:\XXX.flv</param>
    ''' <remarks></remarks>
    ''' 
    Private Sub ConvertFLVNoSound(ByVal InputVideoFile As String, ByVal OutputFLVFile As String)
        'If File.Exists(Application.StartupPath & "\ffmpeg.exe") = True Then
        '    File.Delete(Application.StartupPath & "\ffmpeg.exe")
        'End If
        'UnRar(Application.StartupPath, Application.StartupPath & "\ffmpeg.rar")

        Dim proc As New System.Diagnostics.Process()
        proc.EnableRaisingEvents = False
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.FileName = Application.StartupPath & "\ffmpeg.exe"
        proc.StartInfo.Arguments = " -i " & InputVideoFile & " -vcodec flv  -r 10  -b 100000 -s 320x240    " & OutputFLVFile
        proc.Start()
        proc.WaitForExit()
        proc.Close()
    End Sub
    Private Property AgentVideoFile() As String
        Get
            Return vAgentVideoFile
        End Get
        Set(ByVal value As String)
            vAgentVideoFile = value
        End Set
    End Property
    Private Property CustomerVideoFile() As String
        Get
            Return vCustomerVideoFile
        End Get
        Set(ByVal value As String)
            vCustomerVideoFile = value
        End Set
    End Property
    Private Property SourcePathFile() As String
        Get
            Return vSourcePathFile
        End Get
        Set(ByVal value As String)
            vSourcePathFile = value
        End Set
    End Property
    Private Property DestPathFile() As String
        Get
            Return vDestPathFile
        End Get
        Set(ByVal value As String)
            vDestPathFile = value
        End Set
    End Property

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        DoStop()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        'ContextMenuStrip1.Show()
    End Sub

    Private Function A() As String
        Return "123456789742185fsfgsfdgsfdgsfdgsfgsfgsfgsfgsfdgsfdgsfdgsfsrdfcdrercfdr"
    End Function
    Private Function B() As String
        Return "123456789742185fsfgsfdgsfdgsfdgsfgsfgsfgsfgsfdgsfdgsfdgsfsrdfcdrercfdr"
    End Function
    Private Function C() As String
        Return "123456789742185fsfgsfdgsfdgsfdgsfgsfgsfgsfgsfdgsfdgsfdgsfsrdfcdrercfdr"
    End Function
    Private Function D() As String
        Return "123456789742185fsfgsfdgsfdgsfdgsfgsfgsfgsfgsfdgsfdgsfdgsfsrdfcdrercfdr"
    End Function
    Private Function E() As String
        Return "123456789742185fsfgsfdgsfdgsfdgsfgsfgsfgsfgsfdgsfdgsfdgsfsrdfcdrercfdr"
    End Function


End Class



