Imports System.Data.SqlClient
Imports System.IO
Imports Engine.Common.ShopConnectDBENG
Imports System.Windows.Forms
Imports SoundExtension.Org.Mentalis.Files

Module SoundModule
    Public iniFile As String = Application.StartupPath & "\SoundExtension.ini"
    Public iniDBFile As String = "C:\Windows\QueueSharp.ini"
    Dim logPath = "logs"
    Public Conn As New SqlConnection

    Public DB_SERVER As String = ""
    Public DB_NAME As String = ""
    Public DB_USER As String = ""
    Public DB_PASS As String = ""

    Public DB_SERVER1 As String = ""
    Public DB_NAME1 As String = ""
    Public DB_USER1 As String = ""
    Public DB_PASS1 As String = ""

    Public Structure UpdateConResult
        Dim IsOK As Boolean
        Dim Message As String
    End Structure
    Public Function formatConnectionString(ByVal argIP As String, ByVal argDB As String, ByVal argUser As String, ByVal argPassword As String) As String
        Return "Data Source=" & argIP & ";Initial Catalog=" & argDB & ";Persist Security Info=True;User ID=" & argUser & ";Password=" & argPassword
    End Function

    Public Function CheckConn(ByRef c As SqlConnection, ByVal strConn As String) As UpdateConResult
        Dim uc As New UpdateConResult
        Try
            If c.State = ConnectionState.Open Then c.Close()
        Catch ex As Exception
        End Try

        If c.State <> ConnectionState.Open Then
            If c.State <> ConnectionState.Connecting Then
                c.ConnectionString = strConn
                Try
                    c.Open()
                    uc.IsOK = True
                    uc.Message = ""
                Catch ex As SqlException
                    uc.IsOK = False
                    uc.Message = ex.Message
                End Try
            End If
        End If

        Return uc
    End Function

    Public Function updateConn(ByRef c As SqlConnection) As UpdateConResult
        Dim uc As New UpdateConResult
        Try
            If c.State = ConnectionState.Open Then c.Close()
        Catch ex As Exception
        End Try

        If c.State <> ConnectionState.Open Then
            If c.State <> ConnectionState.Connecting Then
                Dim ini As New IniReader(iniDBFile)
                ini.Section = "SETTING"

                If CheckCurrentDB(iniDBFile) = "MAIN" Then
                    c.ConnectionString = formatConnectionString(ini.ReadString("MainServer"), ini.ReadString("MainDatabase"), ini.ReadString("MainUsername"), Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("MainPassword")))
                    Try
                        c.Open()
                        uc.IsOK = True
                        uc.Message = ""
                    Catch ex As SqlException
                        c.ConnectionString = formatConnectionString(ini.ReadString("DisplayServer"), ini.ReadString("DisplayDatabase"), ini.ReadString("DisplayUsername"), Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("DisplayPassword")))
                        Try
                            c.Open()
                            uc.IsOK = True
                            uc.Message = ""
                        Catch ey As SqlException
                            uc.IsOK = False
                            uc.Message = ey.Message
                        End Try
                    End Try
                Else
                    c.ConnectionString = formatConnectionString(ini.ReadString("DisplayServer"), ini.ReadString("DisplayDatabase"), ini.ReadString("DisplayUsername"), Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("DisplayPassword")))
                    Try
                        c.Open()
                        uc.IsOK = True
                        uc.Message = ""
                    Catch ey As SqlException
                        uc.IsOK = False
                        uc.Message = ey.Message
                    End Try
                End If
            ini = Nothing
            End If
        Else
            uc.IsOK = True
            uc.Message = ""
        End If
        Return uc
    End Function

    Public Function WriteLogFile(ByVal txt As TextBox, Optional ByVal lfileName As String = "QSoundLog_") As String
        Dim ret As String = ""
        If Len(txt.Text) > 0 Then
            Dim path As String = Application.StartupPath & "\" & logPath & "\"
            Try
                If Dir(path) = "" Then
                    MkDir(path)
                End If
                Dim fName As String = path & lfileName & fixDate(Now.Date) & "-" & Guid.NewGuid.ToString & ".txt"
                Dim f As New FileStream(fName, FileMode.Create, FileAccess.Write)
                f.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(txt.Text), 0, Len(txt.Text))
                f.Close()
                ret = "*** LogFile writen Successfully! ***"
            Catch ex As Exception
                ret = "[WRITELOG] - " & ex.Message
            End Try
        Else
            ret = ""
        End If
        Return ret
    End Function
    Public Function fixDate(ByVal argTXT As String) As String
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        If IsDate(argTXT) Then
            Dim dmy As Date = CDate(argTXT)
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

    Public Sub PlaySoundBlink()
        Dim tmp As String = ""
        Dim path As String = System.Windows.Forms.Application.StartupPath & "\Sound\"

        If IO.File.Exists(path & "blink.wav") Then
            Sound.PlayWaveFile(path & "blink.wav")
        End If
    End Sub

    '''<param name="argTxt">คือ ชุดตัวเลข หรือตัวอักษรที่ต้องการให้ออกเสียง
    '''     
    '''           หรือ สัญลักษณ์แทนประโยคต่างๆ เช่น
    '''           ^1 = ขอเชิญคิวหมายเลข
    '''           ^2 = รับบริการที่ห้อง
    '''           ^3 = ค่ะ
    ''' </param>
    '''<param name="argLang">ระบุภาษาที่จะให้ออกเสียง โดย th=ภาษาไทย, en=ภาษาอังกฤษ</param>
    Sub SpeakOut(ByVal argTxt As String, ByVal argLang As String)
        'Maximum Length is 254 character
        Dim sbText As String = Strings.Left(argTxt, 254)
        Dim langPath As String = argLang.ToLower
        Dim tmp As String = "", path As String = System.Windows.Forms.Application.StartupPath & "\Sound\"

        If langPath = "" Then
            langPath = "th"
        End If

        If argTxt = "^1" Then
            '*** ขอเชิญคิวหมายเลข
            If IO.File.Exists(path & "the_queue_number_" & langPath & ".wav") Then
                Sound.PlayWaveFile(path & "the_queue_number_" & langPath & ".wav")
            End If
        ElseIf argTxt = "^2" Then
            '*** รับบริการที่ห้อง
            If IO.File.Exists(path & "please_go_to_" & langPath & ".wav") Then
                Sound.PlayWaveFile(path & "please_go_to_" & langPath & ".wav")
            End If
        ElseIf argTxt = "^3" Then
            '*** ออกเสียง "ค่ะ"
            If IO.File.Exists(path & "ka.wav") Then
                Sound.PlayWaveFile(path & "ka.wav")
            End If

        Else

            'If InStr(sbText, "ห้องเจาะเลือด") > 0 Then
            '    tmp = path & "ห้องเจาะเลือด_" & langPath & ".wav"
            '    Sound.PlayWaveFile(tmp)
            '    sbText = sbText.Replace("ห้องเจาะเลือด", "")
            'ElseIf InStr(sbText, "เจาะเลือด") > 0 Then
            '    'Additional Wording
            '    tmp = path & "เจาะเลือด_" & langPath & ".wav"
            '    Sound.PlayWaveFile(tmp)
            '    sbText = sbText.Replace("เจาะเลือด", "")
            'ElseIf InStr(sbText, "ห้องตรวจ") > 0 Then
            '    tmp = path & "ห้องตรวจ_" & langPath & ".wav"
            '    Sound.PlayWaveFile(tmp)
            '    sbText = sbText.Replace("ห้องตรวจ", "")
            'End If
            '*** ออกเสียงตามอักษร/ตัวเลข
            If IO.File.Exists(path & sbText & "_" & langPath & ".wav") Then
                tmp = path & sbText & "_" & langPath & ".wav"
                Sound.PlayWaveFile(tmp)
            Else
                For i As Int16 = 0 To Len(sbText) - 1
                    tmp = sbText.Substring(i, 1)
                    tmp = path & tmp & "_" & langPath & ".wav"
                    If IO.File.Exists(tmp) Then
                        Sound.PlayWaveFile(tmp)
                    End If
                    Application.DoEvents()
                Next
            End If
        End If
    End Sub
    Public Function myVersion() As String
        With My.Application.Info.Version
            Return .Major & "." & .Minor & "." & .Build & "." & .Revision
        End With

    End Function
End Module
