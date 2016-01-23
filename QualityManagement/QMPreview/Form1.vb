Imports DirectX.Capture
Imports System.Management
Imports System.Management.Instrumentation
Imports System.Threading
Imports System.Collections.Specialized
Imports VoiceRecorder.Audio
Imports NAudio.Wave
Imports AMS
Public Class Form1

    Public liveVolumeThread As System.Threading.Thread
    Public CustomerMICThread As System.Threading.Thread

    Private QueueSharpINI As New Profile.Ini(Application.StartupPath & "\QM\QueueSharp.ini")
    'Private ConfigQM As New Profile.Ini("D:\Scoresolutions\ConfigQM.ini")
    Dim f As Filter
    Dim FlagAgent As Boolean = False
    Dim FlagCustomer As Boolean = False
    Public m_deviceNames As StringCollection
    Public deviceName As String = "(none)"
    Public deviceIndex As Integer = -1

    Public m_sampleDelay As Integer = 100
    Public m_frameDelay As Integer = 10
    Public m_autoStart As Boolean = True
    Public temp_Agent As String
    Public temp_Customer As String

    Private recorderAgent As AudioRecorder()
    Private recorderCustomer As AudioRecorder()
    Private lastPeakAgent As Single
    Private lastPeakCustomer As Single


    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AgentCapture.CaptureInfo.Stop()
            System.IO.File.Delete(temp_Agent)
        Catch ex As Exception

        End Try
        Try
            CustomerCapture.CaptureInfo.Stop()
            System.IO.File.Delete(temp_Customer)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Load Camera List
        Try
            Dispositive = New Filters
            For j As Integer = 0 To Dispositive.VideoInputDevices.Count - 1
                f = Dispositive.VideoInputDevices(j)
                cboAgentInCam.Items.Add(String.Format("{0} # {1}", f.Name, j))
                cboCustomerInCam.Items.Add(String.Format("{0} # {1}", f.Name, j))
            Next
            
            Dim ini As New Profile.Ini(QueueSharpINI.GetValue("Setting", "CamConfig").ToString)
            cboAgentInCam.SelectedIndex = ini.GetValue("Agent", "AgentCameraInput", "")
            cboCustomerInCam.SelectedIndex = ini.GetValue("Customer", "CustomerCameraInput", "")
            ini = Nothing
        Catch ex As Exception
        End Try
        '#Load Mic List
        Try
            Dim ini As New Profile.Ini(QueueSharpINI.GetValue("Setting", "CamConfig").ToString)
            recorderAgent = New AudioRecorder(WaveIn.DeviceCount) {}
            recorderCustomer = New AudioRecorder(WaveIn.DeviceCount) {}
            For i As Integer = 0 To WaveIn.DeviceCount - 1
                cboMICAgent.Items.Add(WaveIn.GetCapabilities(i).ProductName)
                cboMICCustomer.Items.Add(WaveIn.GetCapabilities(i).ProductName)
                recorderAgent(i) = New AudioRecorder 'new object array Agent Wave record
                recorderCustomer(i) = New AudioRecorder 'new object array Customer Wave record
            Next
            For MicAgent As Integer = 0 To WaveIn.DeviceCount - 1
                If InStr(WaveIn.GetCapabilities(MicAgent).ProductName, ini.GetValue("Agent", "AgentMICInput", "")) Then
                    cboMICAgent.SelectedIndex = MicAgent
                End If
            Next
            For MicCustomer As Integer = 0 To WaveIn.DeviceCount - 1
                If InStr(WaveIn.GetCapabilities(MicCustomer).ProductName, ini.GetValue("Customer", "CustomerMICInput", "")) Then
                    cboMICCustomer.SelectedIndex = MicCustomer
                End If
            Next
            ini = Nothing
        Catch ex As Exception

        End Try

        btnAgentPrv_Click(sender, New EventArgs)
        btnCustomerPrv_Click(sender, New EventArgs)
        cboMICAgent_SelectedIndexChanged(sender, New EventArgs)
        cboMICCustomer_SelectedIndexChanged(sender, New EventArgs)
    End Sub

    Private Sub recorderAgent_MaximumCalculated(ByVal sender As Object, ByVal e As MaxSampleEventArgs)
        lastPeakAgent = Math.Max(e.MaxSample, Math.Abs(e.MinSample))
        progMICAgent.Value = CurrentInputAgentLevel
    End Sub
    Public ReadOnly Property CurrentInputAgentLevel() As Single
        Get
            Return lastPeakAgent * 10
        End Get
    End Property
    Private Sub cboMICAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMICAgent.SelectedIndexChanged
        Try
            For i As Integer = 0 To WaveIn.DeviceCount - 1
                RemoveHandler recorderAgent(i).SampleAggregator.MaximumCalculated, AddressOf recorderAgent_MaximumCalculated
            Next
            recorderAgent(cboMICAgent.SelectedIndex).BeginMonitoring(cboMICAgent.SelectedIndex)
            AddHandler recorderAgent(cboMICAgent.SelectedIndex).SampleAggregator.MaximumCalculated, New EventHandler(Of MaxSampleEventArgs)(AddressOf recorderAgent_MaximumCalculated)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub recorderCustomer_MaximumCalculated(ByVal sender As Object, ByVal e As MaxSampleEventArgs)
        lastPeakCustomer = Math.Max(e.MaxSample, Math.Abs(e.MinSample))
        progMICCustomer.Value = CurrentInputCustomerLevel
    End Sub

    Public ReadOnly Property CurrentInputCustomerLevel() As UInteger
        Get
            Return lastPeakCustomer * 10
        End Get

    End Property

    Private Sub cboMICCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMICCustomer.SelectedIndexChanged
        Try
            For i As Integer = 0 To WaveIn.DeviceCount - 1
                RemoveHandler recorderCustomer(i).SampleAggregator.MaximumCalculated, AddressOf recorderCustomer_MaximumCalculated
            Next
            recorderCustomer(cboMICCustomer.SelectedIndex).BeginMonitoring(cboMICCustomer.SelectedIndex)
            AddHandler recorderCustomer(cboMICCustomer.SelectedIndex).SampleAggregator.MaximumCalculated, New EventHandler(Of MaxSampleEventArgs)(AddressOf recorderCustomer_MaximumCalculated)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnAgentPrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgentPrv.Click
        Try
            If FlagAgent = True Then
                AgentCapture.CaptureInfo.Stop()
                AgentCapture.CaptureInfo.DisposeCapture()
            Else
                FlagAgent = True
            End If

            Try
                AgentCapture.Camera = Dispositive.VideoInputDevices(cboAgentInCam.SelectedIndex)
                AgentCapture.CaptureInfo = New Capture(AgentCapture.Camera, Nothing)
                AgentCapture.CaptureInfo.PreviewWindow = Me.panelAgentPreview
                temp_Agent = AgentCapture.CaptureInfo.Filename
                AgentCapture.CaptureInfo.Start()

            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCustomerPrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerPrv.Click
        Try
            If FlagCustomer = True Then

                CustomerCapture.CaptureInfo.Stop()
                CustomerCapture.CaptureInfo.DisposeCapture()
            Else
                FlagCustomer = True
            End If

            Try
                CustomerCapture.Camera = Dispositive.VideoInputDevices(cboCustomerInCam.SelectedIndex)
                CustomerCapture.CaptureInfo = New DirectX.Capture.Capture(CustomerCapture.Camera, Nothing)
                CustomerCapture.CaptureInfo.PreviewWindow = Me.panelCustomerPreview
                temp_Customer = CustomerCapture.CaptureInfo.Filename
                CustomerCapture.CaptureInfo.Start()

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim ini As New Profile.Ini(QueueSharpINI.GetValue("Setting", "CamConfig").ToString)
        ini.SetValue("Agent", "AgentCameraInput", cboAgentInCam.SelectedIndex)
        ini.SetValue("Agent", "AgentMICInput", cboMICAgent.SelectedItem.ToString)
        ini.SetValue("Customer", "CustomerCameraInput", cboCustomerInCam.SelectedIndex)
        ini.SetValue("Customer", "CustomerMICInput", cboMICCustomer.SelectedIndex.ToString)
        ini = Nothing
    End Sub

    Private Sub VideoCompressionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoCompressionToolStripMenuItem.Click
        Try
            If FlagAgent = True Then
                AgentCapture.CaptureInfo.Stop()
                'AgentCapture.CaptureInfo.DisposeCapture()
            End If
            If AgentCapture.CaptureInfo Is Nothing Then
                AgentCapture.Camera = Dispositive.VideoInputDevices(0)
                AgentCapture.CaptureInfo = New DirectX.Capture.Capture(AgentCapture.Camera, Nothing)
            End If
            For c As Integer = 0 To Dispositive.VideoCompressors.Count - 1
                f = Dispositive.VideoCompressors(c)
                If f.Name = "ffdshow video encoder" Then
                    AgentCapture.CaptureInfo.VideoCompressor = Dispositive.VideoCompressors(c)
                    Exit For
                End If
            Next
            AgentCapture.CaptureInfo.PropertyPages(2).Show(Me)
        Catch ex As Exception

        End Try

    End Sub


End Class
