Imports AMS
Imports DirectX.Capture

Public Class HostWebcam
    Private HostCapture As Capture = Nothing
    Private HostFilename As String
    Private VideoInputDevice As Integer = 0
    Private vSetCueFlag As Boolean = False
    Private vCaptureFlag As Boolean = False
    Private vInitialFlag As Boolean = False
    Private vVideoCompressorFlag As Boolean = False
    Private pCheckHost As Boolean
    'Dim ConfigINI As New Profile.Ini("D:\Scoresolutions\ConfigQM.ini")
    Sub New()

    End Sub
#Region "DetectCamera"
    'Public Function DetectCamera() As Boolean
    '    Dim classEnum As IEnumMoniker = Nothing

    '    Dim devEnum As ICreateDevEnum = CType(New CreateDevEnum, ICreateDevEnum)
    '    Dim hr As Integer = 0
    '    hr = devEnum.CreateClassEnumerator(FilterCategory.VideoInputDevice, classEnum, 0)

    '    If classEnum Is Nothing Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
#End Region
#Region "Accesscamera"
    'Public Function AccessCamera() As Boolean
    '    Dim hr As Integer = 0
    '    Dim classEnum As IEnumMoniker = Nothing
    '    Dim moniker As IMoniker() = New IMoniker(0) {}
    '    Dim source As Object = Nothing
    '    Dim devEnum As ICreateDevEnum = CType(New CreateDevEnum, ICreateDevEnum)
    '    hr = devEnum.CreateClassEnumerator(FilterCategory.VideoInputDevice, classEnum, 0)
    '    If classEnum.Next(moniker.Length, moniker, IntPtr.Zero) = 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
#End Region
    ''' <summary>
    ''' สำหรับตรวจสอบความพร้อมของกล้องตัวแรก
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeHostWebcam(ByVal InputDeviceIndex As Integer, ByVal QNo As String) As Boolean
        Try
            Dim size As New Size(320, 240)
            If CheckVideoCompressors(InputDeviceIndex) = True Then
                SetVideoCompressors = True
            Else
                SetVideoCompressors = False
            End If
            HostCapture.FrameSize = size
            HostCapture.FrameRate = CDbl(15)

            If HostCue(QNo) = True Then
                CheckHost = True
                Return True
            Else
                CheckHost = False
                Return False
            End If
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.HostWebcam.InitializeHostWebcam : Exception : " & ex.Message & "HostCapture Stopped :" & HostCapture.Stopped, "")
            CheckHost = False
            Return False
        End Try
    End Function
    Public Property CheckHost() As Boolean
        Get
            Return pCheckHost
        End Get
        Set(ByVal value As Boolean)
            pCheckHost = value
        End Set
    End Property
    Private Function CheckVideoCompressors(ByVal InputDeviceIndex As Integer) As Boolean
        Try
            Dim HostFilters As New DirectX.Capture.Filters()
            Dim f As Filter
            Dim a As Filter
            Dim v As Filter

            Dim audioIndex As Integer
            Dim videoIndex As Integer

            Dim ini As New Profile.Ini(Application.StartupPath & "\QM\QueueSharp.ini")
            Dim ConfigINI As New Profile.Ini(ini.GetValue("Setting", "CamConfig").ToString)
            For c As Integer = 0 To HostFilters.AudioInputDevices.Count - 1
                a = HostFilters.AudioInputDevices(c)
                If InStr(a.Name, ConfigINI.GetValue("Agent", "AgentMICInput")) > 0 Then
                    audioIndex = c
                    Exit For
                End If
            Next
            ini = Nothing
            ConfigINI = Nothing

            For b As Integer = 0 To HostFilters.VideoInputDevices.Count - 1
                v = HostFilters.VideoInputDevices(b)
                If InStr(v.Name, "C170") > 0 Then
                    videoIndex = b
                    Exit For
                End If
            Next
            HostCapture = New Capture(HostFilters.VideoInputDevices(InputDeviceIndex), HostFilters.AudioInputDevices(audioIndex))
            For c As Integer = 0 To HostFilters.VideoCompressors.Count - 1
                f = HostFilters.VideoCompressors(c)
                If InStr(f.Name, "Xvid") > 0 Then
                    HostCapture.VideoCompressor = HostFilters.VideoCompressors(c)
                    Exit For
                End If
            Next

            If HostCapture.VideoCompressor Is Nothing Then
                'Throw New ArgumentException("Please Reinstall ffdshow video encoder Tools")
                'Throw New Exception
                Return False
            End If
            Return True
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.HostWebcam.CheckVideoCompressors : " & ex.Message, "")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' สำหรับเตรียมพร้อมก่อนเริ่มการบันทึก ช่วยทำให้การเรียก method start รวดเร็วขึ้น
    ''' </summary>
    Private Function HostCue(ByVal QNo As String) As Boolean
        Try
            If HostCapture Is Nothing Then
                Return False
            Else
                If Not HostCapture.Cued Then
                    Dim tempfile As String = (Application.StartupPath & "\QM\Temp\" & QNo & "A.avi")
                    HostCapture.Filename = tempfile
                    HostFile = tempfile
                    SetCueFlag = True
                End If

                HostCapture.Cue()
                Return True
            End If
        Catch ex As Exception
            SaveQueryErrorLog("QueueSharp.HostWebcam.HostCue : Exception : " & ex.Message & " HostCapture.Stopped: " & HostCapture.Stopped.ToString & " HostCapture.Cued:" & HostCapture.Cued.ToString, "")
            Return False
        End Try
    End Function
    ''' <summary>
    ''' คำสั่งให้กล้องเริ่มการบันทึก
    ''' </summary>
    Public Sub HostStart()
        Try
            If HostCapture Is Nothing Then
                'Return False
            Else
                If SetCueFlag = True And SetCaptureFlag = False Then
                    HostCapture.Start()
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

    Public Function HostStop() As Boolean
        Try
            If SetCaptureFlag = True Then
                HostCapture.Stop()
                HostCapture.Dispose()
                'HostfileByteArray = FileToByteArray(HostFile)
                'If HostfileByteArray Is Nothing Then
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

    Public Function HostDispose() As Boolean
        Try
            HostCapture.Dispose()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    'Private Sub ErrorLogDB(ByVal errmsg As String)
    '    Dim sqlstr As String
    '    Dim i As Integer

    '    sqlstr = "declare @ID as int;select @ID = (select isnull(MAX(id + 1),1) as id from TB_Error_Log);"
    '    sqlstr = sqlstr & "Insert Into TB_Error_Log (id,client_ip, log_date,error_message,version) Values (@ID,'" & GetIPAddress() & "',GetDate(),'" & errmsg & "','" & getMyVersion() & "')"
    '    i = New DBClass().SqlExecute(sqlstr)


    'End Sub
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
    Public Sub ShowEncoderConfig(ByVal formobj As Object)
        If SetVideoCompressors = True Then
            HostCapture.PropertyPages(2).Show(formobj)
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

    Public Property HostFile() As String
        Get
            Return HostFilename
        End Get
        Set(ByVal value As String)
            HostFilename = value
        End Set
    End Property
    Private Property SetCueFlag() As Boolean
        Get
            Return vSetCueFlag
        End Get
        Set(ByVal value As Boolean)
            vSetCueFlag = value
        End Set
    End Property
    Private Property SetCaptureFlag() As Boolean
        Get
            Return vCaptureFlag
        End Get
        Set(ByVal value As Boolean)
            vCaptureFlag = value
        End Set
    End Property
    Private Property InitialFlag() As Boolean
        Get
            Return vInitialFlag
        End Get
        Set(ByVal value As Boolean)
            vInitialFlag = value
        End Set
    End Property
    Private Property SetVideoCompressors() As Boolean
        Get
            Return vVideoCompressorFlag
        End Get
        Set(ByVal value As Boolean)
            vVideoCompressorFlag = value
        End Set
    End Property
End Class
