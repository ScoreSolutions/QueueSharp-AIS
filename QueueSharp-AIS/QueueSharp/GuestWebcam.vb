Imports AMS
Imports DirectX.Capture

Public Class GuestWebcam
    Private GuestCapture As Capture = Nothing
    Private vGuestFilename As String
    Private vSetCue As Boolean = False
    Private vCapture As Boolean = False
    Private vVideoCompressorsFlag As Boolean = False
    Private pCheckGuest As Boolean
    'Dim ConfigINI As New Profile.Ini("D:\Scoresolutions\ConfigQM.ini")
    Sub New()

    End Sub
    ''' <summary>
    ''' สำหรับตรวจสอบความพร้อมของกล้อง#2
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeGuestWebcam(ByVal InputDeviceIndex As Integer, ByVal Qid As String) As Boolean
        Try
            Dim size As New Size(320, 240)
            If CheckVideoCompressors(InputDeviceIndex) = True Then
                SetvideoCompressors = True
            Else
                SetvideoCompressors = False
            End If
            GuestCapture.FrameSize = size
            GuestCapture.FrameRate = CDbl(15)
            If GuestCue(Qid) = True Then
                CheckGuest = True
                Return True
            Else
                CheckGuest = False
                Return False
            End If
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.GuestWebcam.InitializeHostWebcam : Exception : " & ex.Message & " GuestCapture.Stopped :" & GuestCapture.Stopped, "")
            CheckGuest = False
            Return False
        End Try
    End Function
    Public Property CheckGuest() As Boolean
        Get
            Return pCheckGuest
        End Get
        Set(ByVal value As Boolean)
            pCheckGuest = value
        End Set
    End Property
    Private Function CheckVideoCompressors(ByVal InputDeviceIndex As Integer) As Boolean
        Try
            Dim GuestFilters As New Filters()
            Dim f As Filter
            Dim a As Filter
            Dim v As Filter
            Dim audioIndex As Integer
            Dim videoIndex As Integer
            Dim ini As New Profile.Ini(Application.StartupPath & "\QM\QueueSharp.ini")
            Dim ConfigINI As New Profile.Ini(ini.GetValue("Setting", "CamConfig").ToString)
            For c As Integer = 0 To GuestFilters.AudioInputDevices.Count - 1
                a = GuestFilters.AudioInputDevices(c)
                If InStr(a.Name, ConfigINI.GetValue("Customer", "CustomerMICInput")) > 0 Then
                    audioIndex = c
                    Exit For
                End If
            Next
            ini = Nothing
            ConfigINI = Nothing

            For b As Integer = 0 To GuestFilters.VideoInputDevices.Count - 1
                v = GuestFilters.VideoInputDevices(b)
                If InStr(v.Name, "USB Video") > 0 Then
                    videoIndex = b
                    Exit For
                End If
            Next

            GuestCapture = New Capture(GuestFilters.VideoInputDevices(InputDeviceIndex), GuestFilters.AudioInputDevices(audioIndex))
            For c As Integer = 0 To GuestFilters.VideoCompressors.Count - 1
                f = GuestFilters.VideoCompressors(c)
                If InStr(f.Name, "Xvid") > 0 Then
                    GuestCapture.VideoCompressor = GuestFilters.VideoCompressors(c)
                    Exit For
                End If
            Next

            If GuestCapture.VideoCompressor Is Nothing Then
                'Throw New Exception
                Return False
            End If
            Return True
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.GuestWebcam.CheckVideoCompressors : " & ex.Message, "")
            Return False
        End Try

    End Function
    Public Function GuestCue(ByVal Qid As String) As Boolean
        Try
            If GuestCapture Is Nothing Then
                Return False
            Else
                If Not GuestCapture.Cued Then
                    Dim tempfile As String = (Application.StartupPath & "\QM\Temp\" & Qid & "B.avi")
                    GuestCapture.Filename = tempfile
                    GuestFilename = tempfile
                    SetCueFlag = True
                End If
                GuestCapture.Cue()
                Return True
            End If
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.GuestWebcam.GuestCue : Exception : " & ex.Message & " GuestCapture.Stopped: " & GuestCapture.Stopped.ToString & " GuestCapture.Cued:" & GuestCapture.Cued.ToString, "")
            Return False
        End Try
    End Function
    ''' <summary>
    ''' คำสั่งให้กล้อง#2 เริ่มการบันทึก
    ''' </summary>
    Public Sub GuestStart()
        Try
            If GuestCapture Is Nothing Then
                'Return False
            Else
                If SetCueFlag = True And SetCaptureFlag = False Then
                    GuestCapture.Start()
                    SetCaptureFlag = True
                    'Return True
                Else
                    'Return False
                End If
            End If
        Catch ex As Exception
            'Return False
        End Try
    End Sub
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
    ''' <summary>
    ''' คำสั่งให้กล้อง#2 หยุดการบันทึก
    ''' </summary>
    Public Function GuestStop() As Boolean
        Try
            If SetCaptureFlag = True Then
                GuestCapture.Stop()
                GuestCapture.Dispose()
                'GuestFileByteArray = FileToByteArray(GuestFilename)
                'If GuestFileByteArray Is Nothing Then
                '    Return False
                'End If
                SetCaptureFlag = False
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public ReadOnly Property GuestStoped() As Boolean
        Get
            Return GuestCapture.Stopped
        End Get
    End Property

    Public Function GuestDispose() As Boolean
        Try
            GuestCapture.Dispose()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Sub ShowEncoderConfig(ByVal formobj As Object)
        If SetvideoCompressors = True Then
            GuestCapture.PropertyPages(2).Show(formobj)
        End If
    End Sub
    ''' <summary>
    ''' Convert Source File To Byte()
    ''' </summary>
    ''' <param name="_FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' สำหรับบันทึกไฟล์วีดีโอ
    ''' </summary>
    ''' <param name="Filename">ชื่อ Path และ Filename Ex."C:\xxx.avi"</param>
    ''' <param name="byteData">ข้อมูลที่เป็น Byte Array</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Private Property GuestFilename() As String
        Get
            Return vGuestFilename
        End Get
        Set(ByVal value As String)
            vGuestFilename = value
        End Set
    End Property
    Private Property SetCueFlag() As Boolean
        Get
            Return vSetCue
        End Get
        Set(ByVal value As Boolean)
            vSetCue = value
        End Set
    End Property
    Private Property SetCaptureFlag() As Boolean
        Get
            Return vCapture
        End Get
        Set(ByVal value As Boolean)
            vCapture = value
        End Set
    End Property
    Private Property SetvideoCompressors() As Boolean
        Get
            Return vVideoCompressorsFlag
        End Get
        Set(ByVal value As Boolean)
            vVideoCompressorsFlag = value
        End Set
    End Property
End Class
