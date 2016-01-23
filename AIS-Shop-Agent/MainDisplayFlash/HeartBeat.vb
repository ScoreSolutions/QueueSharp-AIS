Imports System.IO
Imports System.Threading
Imports System.Globalization

Public Module HeartBeat

    Function UpdateHB(ByVal Patch As String, ByVal ProcessName As String) As Boolean
        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Try
            Dim FindFolder() As String = Patch.Split("\")
            Dim Folder As String = ""
            For i As Int32 = 0 To FindFolder.Length - 2
                If i < FindFolder.Length - 2 Then
                    Folder += FindFolder(i) & "\"
                Else
                    Folder += FindFolder(i)
                End If
            Next

            Dim DirInfo As New DirectoryInfo(Folder)
            '*** Create Folder ***'
            If Not DirInfo.Exists Then
                DirInfo.Create()
            End If

            Dim txt As String = ProcessName & "|" & FixDate(Date.Now) & " " & Date.Now.ToString("HH:mm:ss")
            Dim ReadFile As New StreamReader(Patch, System.Text.Encoding.UTF8)
            Dim CurrentLine As String
            Dim AllText As String = ""
            Dim CheckProcess As Int32 = 0
            While ReadFile.Peek <> -1
                Dim CheckCurrentLine() As String
                CurrentLine = ReadFile.ReadLine
                CheckCurrentLine = CurrentLine.Split("|")
                If CheckCurrentLine(0).Trim = ProcessName.Trim Then
                    AllText += txt & vbNewLine
                    CheckProcess += 1
                Else
                    AllText += CurrentLine & vbNewLine
                End If
            End While
            ReadFile.Close()
            If CheckProcess = 0 Then
                AllText += txt & vbNewLine
            End If

            WriteTextfile(Patch, AllText)
            Return True
        Catch ex As Exception : End Try

        Return False
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

    Public Function FixDate(ByVal StringDate As String) As String
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        If IsDate(StringDate) Then
            Dim dmy As Date = CDate(StringDate)
            d = dmy.Day
            m = dmy.Month
            y = dmy.Year
            If y > 2500 Then
                y = y - 543
            End If
            Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
        Else
            Return ""
        End If

    End Function
    Public Sub WriteTextfile(ByVal Patch As String, ByVal ValTxt As String)
        Try
            If ValTxt = "" Then
                ValTxt = " "
            End If
            Dim txtFile As New StreamWriter(patch, False, System.Text.Encoding.UTF8, ValTxt.Length)
            With txtFile
                .Write(ValTxt)
                .Close()
            End With
        Catch ex As Exception : End Try

    End Sub

    Public Sub ReadTextfile(ByVal patch As String)
        Try

            
        Catch ex As Exception : End Try

    End Sub

End Module
