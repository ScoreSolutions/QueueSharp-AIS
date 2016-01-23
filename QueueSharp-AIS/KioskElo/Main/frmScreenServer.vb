Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DirectShowLib

Public Class frmScreenServer

    Enum PlayState
        Stopped
        Paused
        Running
        Init
    End Enum

    Enum MediaType
        Audio
        Video
    End Enum

    Private graphBuilder As IGraphBuilder = Nothing
    Private Const WMGraphNotify As Integer = 13
    Private Const VolumeFull As Integer = 0
    Private Const VolumeSilence As Integer = -10000
    Private mediaControl As IMediaControl = Nothing
    Private mediaEventEx As IMediaEventEx = Nothing
    Private videoWindow As IVideoWindow = Nothing
    Private basicAudio As IBasicAudio = Nothing
    Private basicVideo As IBasicVideo = Nothing
    Private mediaSeeking As IMediaSeeking = Nothing
    Private mediaPosition As IMediaPosition = Nothing
    Private frameStep As IVideoFrameStep = Nothing
    Private filename As String = String.Empty
    Private isAudioOnly As Boolean = False
    Private isFullScreen As Boolean = False
    Private currentVolume As Integer = VolumeFull
    Private currentState As PlayState = PlayState.Stopped
    Private currentPlaybackRate As Double = 1.0
    Private UseHand As IntPtr
    Private UseCtrl As System.Windows.Forms.Control
    Private FsDrain As IntPtr = IntPtr.Zero
    Private Event MedClose()
#If DEBUG Then
    Private rot As DsROTEntry = Nothing
#End If

    Private Sub frmScreenServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PlayMedia(Application.StartupPath & "\AIS.FLV")
        FullScreenSwitch()
    End Sub

    Private Sub PlayMedia(ByVal fName As String)
        Dim hr As Integer = 0
        If fName = Nothing Then Exit Sub
        Try
            graphBuilder = DirectCast(New FilterGraph, IFilterGraph2) 'Load Graph Builder Device

            hr = graphBuilder.RenderFile(fName, Nothing) ' Initialize Graph Builder
            DsError.ThrowExceptionForHR(hr)

            'Load all Interfaces we will use
            mediaControl = DirectCast(graphBuilder, IMediaControl)
            mediaEventEx = DirectCast(graphBuilder, IMediaEventEx)
            mediaSeeking = DirectCast(graphBuilder, IMediaSeeking)
            mediaPosition = DirectCast(graphBuilder, IMediaPosition)
            videoWindow = DirectCast(graphBuilder, IVideoWindow)
            basicAudio = DirectCast(graphBuilder, IBasicVideo)
            basicVideo = DirectCast(graphBuilder, IBasicAudio)

            Call CheckType() 'Check to See if Audio or Video Call

            If isAudioOnly = False Then
                'Notfy Window of Video
                hr = mediaEventEx.SetNotifyWindow(UseHand, WMGraphNotify, IntPtr.Zero)
                DsError.ThrowExceptionForHR(hr)

                'Set Owner to Display Video
                hr = videoWindow.put_Owner(UseHand)
                DsError.ThrowExceptionForHR(hr)

                'Set Owner Video Style
                hr = videoWindow.put_WindowStyle(WindowStyle.Child And WindowStyle.ClipSiblings And WindowStyle.ClipChildren)
                DsError.ThrowExceptionForHR(hr)
            End If

#If DEBUG Then
            rot = New DsROTEntry(graphBuilder)
#End If

            Me.Focus()
            'Update Form Title
            Call SetFormTitle()

            'Start Media
            hr = mediaControl.Run
            DsError.ThrowExceptionForHR(hr)

            currentState = PlayState.Running

            If isAudioOnly = False Then
                'Set Video Size
                hr = VideoWindowSize(1, 1)
                DsError.ThrowExceptionForHR(hr)
            End If
        Catch ex As Exception
            MsgBox("Error " & ex.Message, MsgBoxStyle.Critical, "Error")
            RaiseEvent MedClose()
        End Try
    End Sub

    Private Function VideoWindowSize(ByVal nMultiplier As Integer, ByVal nDivider As Integer) As Integer
        Try
            Dim hr As Integer = 0
            Dim lHeight As Integer, lWidth As Integer
            'Get Video Size
            hr = basicVideo.GetVideoSize(lWidth, lHeight)
            If hr = DsResults.E_NoInterface Then Return 0 : Exit Function

            'Change if Different Size is selected in menu(50%, 200%..)
            lWidth = lWidth * nMultiplier / nDivider
            lHeight = lHeight * nMultiplier / nDivider

            'Set Window Size video will play on
            UseCtrl.ClientSize = New Size(lWidth, lHeight)
            Application.DoEvents()

            'Set Video Position on Window
            hr = videoWindow.SetWindowPosition(0, 0, 800, 600)
            Return hr
        Catch ex As Exception
            'MsgBox("Error " & ex.Message, MsgBoxStyle.Critical, "Error")
            RaiseEvent MedClose()
        End Try
    End Function

    Private Sub SetFormTitle()
        Dim hr As Integer = 0
        If filename = Nothing Then
            Me.Text = "Media Player"
        Else
            Dim MedTitle As String = ""

            If isAudioOnly = True Then
                MedTitle = "Audio"
            Else
                MedTitle = "Video"
            End If

            If currentState = PlayState.Paused Then
                MedTitle = MedTitle & " - Paused - "
            End If
            If currentState = PlayState.Running Then
                MedTitle = MedTitle & " - Playing - "
            End If
            If currentState = PlayState.Stopped Then
                MedTitle = MedTitle & " - Stopped - "
            End If

            Me.Text = MedTitle & filename
            MedTitle = Nothing
        End If
    End Sub

    Private Sub CheckType()
        Try
            Dim hr As Integer = 0
            Dim lVisible As OABool

            'If Interface is Nothing then Media is Audio
            If basicVideo Is Nothing Or videoWindow Is Nothing Then
                isAudioOnly = True
            Else
                isAudioOnly = False
            End If

            'Another way to test if Audio or Video
            hr = videoWindow.get_Visible(lVisible)
            If hr < 0 Then
                isAudioOnly = True
            End If
        Catch ex As Exception
            MsgBox("Errpr " & ex.Message, MsgBoxStyle.Critical, "Error")
            RaiseEvent MedClose()
        End Try
    End Sub

    Private Sub FullScreenSwitch()
        Try
            'Dont Bother with FullScreen if Playing just Audio
            If isAudioOnly = True Then Exit Sub
            Dim hr As Integer = 0

            If isFullScreen = False Then

                'Get Message Drain
                hr = videoWindow.get_MessageDrain(FsDrain)
                DsError.ThrowExceptionForHR(hr)

                'Send Message Drain to My Handle to allow keypress to exit fullscreen
                hr = videoWindow.put_MessageDrain(UseHand)
                DsError.ThrowExceptionForHR(hr)

                'Switch to Fullscreen Mode
                hr = videoWindow.put_FullScreenMode(OABool.True)
                DsError.ThrowExceptionForHR(hr)

                isFullScreen = True
            Else

                'Switch to Window Mode
                hr = videoWindow.put_FullScreenMode(OABool.False)
                DsError.ThrowExceptionForHR(hr)

                'Set Message Drain back to Normal
                hr = videoWindow.put_MessageDrain(FsDrain)
                DsError.ThrowExceptionForHR(hr)

                isFullScreen = False
            End If
        Catch ex As Exception
            MsgBox("Error " & ex.Message, MsgBoxStyle.Critical, "Error")
            RaiseEvent MedClose()
        End Try
    End Sub

    Private Sub frmScreenServer_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        RaiseEvent MedClose()
    End Sub

    Private Sub frmScreenServer_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        RaiseEvent MedClose()
    End Sub
End Class