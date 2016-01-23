Public Class frmSplash



    Sub updateNotFound(ByVal cnt as integer)
        lblNotFound.Text = "File Not Found - [" & cnt & "]"
        lblNotFound.Refresh()
    End Sub

    Function chkExists(ByVal f As String) As Boolean
        If IO.File.Exists(f) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub frmSplash_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        frmMain.Refresh()
        Me.Refresh()
        lblStat.Text = ""

        'Check Sound Blink.mp3
        Dim path As String = Windows.Forms.Application.StartupPath & "\Sound\"
        Dim fname As String = path & "blink.wav"
        lblStat.Text = "Checking File: " & Strings.Right("..." & fname, 14) : lblStat.Refresh()
        If Not chkExists(fname) Then
            updateNotFound(1)
        End If

        lblStat.Text = "Checking Database Connection ..."
        Dim tmp As New UpdateConResult
        tmp = SoundModule.updateConn(Conn)

        If tmp.IsOK Then
            lblStat.Text = "Checking Database Connection ...OK"
            System.Threading.Thread.Sleep(1000)
            lblStat.Text = "Deleting Old Data..."
            Dim sql As String = "delete from tb_speaker "
            sql += " where call_date < getdate()"
            'sql += " where convert(varchar(8),call_date,112) < convert(varchar(8),getdate(),112)"
            Dim cmd As New SqlClient.SqlCommand(sql)
            cmd.Connection = Conn
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                frmMain.uplog(ex.Message)
            End Try
        Else
            lblStat.Text = "Checking Database Connection ...FAILED!!!"
        End If
        lblStat.Refresh()
        System.Threading.Thread.Sleep(1000)

        Me.Close()
        frmMain.WindowState = FormWindowState.Minimized
    End Sub

End Class