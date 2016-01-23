Option Explicit On
Imports DirectX.Capture
Imports System.Collections.Specialized
Module ModCAPTURE
    Public Structure Active
        Dim Camera As Filter
        Dim CaptureInfo As Capture
        Dim Counter As Integer
        Dim CounterFrames As Integer
        Dim PathVideo As String
    End Structure

    Public AgentCapture As Active
    Public CustomerCapture As Active
    Public Dispositive As Filters
End Module
