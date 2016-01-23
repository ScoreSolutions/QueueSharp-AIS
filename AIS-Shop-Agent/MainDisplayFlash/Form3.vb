Imports System.IO
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Public Class Form3
    Dim sContentsHead As String
    Dim sContentsFooter As String = ""
    Dim sContents As String
    Dim sSerenadeHead As String
    Dim sSerenadeContents As String
    Dim sSerenadeFooter As String
    Dim sErr As String
    Dim bAns As Boolean
    Dim pLoopInterval As Integer
    Dim pLoopCount As Integer
    Dim totalRec As Integer
    Dim Looprec As Integer
    Dim startRow As Integer
    Dim endRow As Integer
    Dim startRowSerenade As Integer
    Dim endrowSerenade As Integer
    Dim iTotalServiceCount As Integer
    Dim FlagPlay As Boolean = False
    Dim refreshNextQ As Boolean = False
    Dim refreshNextQ2 As Boolean = False
    Dim NewDatatable As New DataTable
    Dim OldDatatable As New DataTable
    Dim NewDTRegisNextQ As New DataTable
    Dim OldDTRegisNextQ As New DataTable
    Dim sqlstr As String
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim dtNextQ As New DataTable
    Dim dtv As New DataView
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
        Timer2.Enabled = True
        StartRec = 1
        startRow = StartRec
        StartRecSeranade = 1
        startRowSerenade = StartRecSeranade
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

        Dim Contents As String
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

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next

        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")

        ds = New DBClass().SqlGet("exec SP_MainDisplayShop", "tb_MainDisplay")
        dt = ds.Tables("tb_MainDisplay")
        NewDatatable = dt


        ds = New DBClass().SqlGet("exec SP_MainDisplayNextQueue", "tb_NextQ")
        dtNextQ = ds.Tables("tb_NextQ")
        NewDTRegisNextQ = dtNextQ


        sContentsHead = ""
        sContents = ""
        startRow = StartRec

        endRow = startRow + dt.Rows.Count
        If EndRec > dt.Rows.Count Then
            endRow = dt.Rows.Count
        End If
        Dim j As Integer = 1
        Dim i As Integer = 1
        sContentsHead = "CurrentDate=" & Now.ToString("dd/MM/yyyy") & "&CurrentTime=" & Now.ToString("HH:mm")

        For i = startRow To endRow Step 4
            If i < endRow Then
                sContentsHead = sContentsHead & "&TextService_th" & j & "=" & dt.Rows(i - 1).Item("item_name_th") & "&TextService_en" & j & "=" & dt.Rows(i - 1).Item("item_name_en")

            End If
            j = j + 1
        Next

        sContents = ""
        Dim k As Integer = 1
        Dim m As Integer = 1
        For m = startRow To endRow

            If m <= endRow Then

                sContents = sContents & "&Q" & dt.Rows(m - 1).Item("row") & "_" & k & "=" & dt.Rows(m - 1).Item("queue") & "&C" & dt.Rows(m - 1).Item("row") & "_" & k & "=" & dt.Rows(m - 1).Item("counter")

            End If

            If dt.Rows(m - 1).Item("row") = 4 Then
                k = k + 1

            End If

        Next
        '----------------- Compare ข้อมูลชุดใหม่ กับ ชุดเก่า ------------------------------------------

        Dim r1 = NewDatatable.AsEnumerable
        Dim r2 = OldDatatable.AsEnumerable

        Dim ExceptResult = r1.Except(r2, DataRowComparer.Default)
        Dim IntersectResult = r2.Intersect(r1, DataRowComparer.Default)

        Dim dtResult = ExceptResult.CopyToDataTable()
        Dim dtIntersect = IntersectResult.CopyToDataTable()

        If dtResult IsNot Nothing Then

            OldDatatable = NewDatatable
            refreshNextQ = True
        Else
            refreshNextQ = False
        End If

        '----------------------Compare ข้อมูล NextQ ชุดเก่ากับชุดใหม่-------------------------------------------------
        Dim a1 = NewDTRegisNextQ.AsEnumerable
        Dim a2 = OldDTRegisNextQ.AsEnumerable

        Dim ExceptResultNextQ = a1.Except(a2, DataRowComparer.Default)
        Dim IntersectResultNextQ = a2.Intersect(a2, DataRowComparer.Default)

        Dim dtResultNextQ = ExceptResultNextQ.CopyToDataTable
        Dim dtIntersectNextQ = IntersectResultNextQ.CopyToDataTable
        dtv = OldDTRegisNextQ.DefaultView
        If dtResultNextQ IsNot Nothing Then
            dtv.RowFilter = "queue_no='" & dtResultNextQ.Rows(0).Item("queue_no") & "'"

            If dtv.Count = 0 Then
                OldDTRegisNextQ = NewDTRegisNextQ
                refreshNextQ2 = True
            Else
                refreshNextQ2 = False
            End If

        Else
            refreshNextQ2 = False
        End If

        '--------------------------------------------------------------------------------------------------------

        If refreshNextQ = True Or refreshNextQ2 = True Then 'ถ้ามีการเปลี่ยนแปลงของชุดเก่าและชุดใหม่ ให้มีการอัพเดตข้อมูล คิวถัดไป ในหน้าจอ Maindisplay

            sContentsFooter = ""
            For n As Integer = 0 To 3
                If dtNextQ.Rows.Count >= n + 1 Then
                    sContentsFooter = sContentsFooter & "&Queue" & n + 1 & "_5=" & dtNextQ.Rows(n).Item("queue_no") & "&QTime" & n + 1 & "_5=" & dtNextQ.Rows(n).Item("time")
                Else
                    sContentsFooter = sContentsFooter & "&Queue" & n + 1 & "_5=&QTime" & n + 1 & "_5="
                End If

            Next

        End If

        sContents = sContentsHead & sContents & sContentsFooter
        bAns = SaveTextToFile(sContents, My.Settings.InputPath, sErr)

        StartRec = endRow
        EndRec = endRow + dt.Rows.Count
        If StartRec >= dt.Rows.Count Then
            StartRec = 1
        End If


    End Sub
    Private Sub ShowDummyHead(ByVal rowend)

    End Sub
    Private Sub ShowDummy(ByVal rowend)

    End Sub

    Private Sub SettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingToolStripMenuItem.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Dim f As New Form2
        f.ShowDialog()
    End Sub

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click
        Timer1.Interval = (My.Settings.Interval * 1000) + 1
        Timer1.Enabled = True
        Timer2.Interval = (My.Settings.Interval * 1000) + 1
        Timer2.Enabled = True


    End Sub

    Private Sub SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click

        End
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        On Error Resume Next
        Dim sqlstr As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")

        ds = New DBClass().SqlGet("exec SP_MainDisplaySerenade", "tb_MainDisplay")
        dt = ds.Tables("tb_MainDisplay")
        sSerenadeHead = ""
        sSerenadeContents = ""
        startRowSerenade = StartRecSeranade

        endrowSerenade = startRowSerenade + dt.Rows.Count

        Dim j As Integer = 1
        Dim i As Integer = 1
        sSerenadeHead = ""
        sSerenadeHead = "CurrentDate=" & Now.ToString("dd/MM/yyyy") & "&CurrentTime=" & Now.ToString("HH:mm")
        For i = startRowSerenade To endrowSerenade
            If i < endrowSerenade Then
                sSerenadeHead = sSerenadeHead & "&TextService_th" & j & "=" & dt.Rows(i - 1).Item("item_name_th") & "&TextService_en" & j & "=" & dt.Rows(i - 1).Item("item_name_en")

            End If
            j = j + 1
        Next
        sSerenadeContents = ""
        Dim k As Integer = 1
        Dim n As Integer = 1
        For n = startRowSerenade To endrowSerenade
            If n < endrowSerenade Then
                If sSerenadeContents = "" Then
                    sSerenadeContents = "&Q" & k & "_" & 1 & "=" & dt.Rows(n - 1).Item("queue") & "&C" & k & "_" & 1 & "=" & dt.Rows(n - 1).Item("counter")

                Else
                    sSerenadeContents = sSerenadeContents & "&Q" & k & "_" & 1 & "=" & dt.Rows(n - 1).Item("queue") & "&C" & k & "_" & 1 & "=" & dt.Rows(n - 1).Item("counter")
                End If
                k = k + 1
            End If

        Next

        sSerenadeContents = sSerenadeHead & sSerenadeContents
        bAns = SaveTextToFile(sSerenadeContents, My.Settings.InputSerenade, sErr)
        StartRecSeranade = endrowSerenade
        EndRecSerenade = endrowSerenade + dt.Rows.Count
        If StartRecSeranade >= dt.Rows.Count Then
            StartRecSeranade = 1
        End If



    End Sub

    Public Property StartRec() As Integer
        Get
            Return startRow
        End Get
        Set(ByVal value As Integer)
            startRow = value
        End Set
    End Property
    Public Property EndRec() As Integer
        Get
            Return endRow
        End Get
        Set(ByVal value As Integer)
            endRow = value
        End Set
    End Property
    Public Property StartRecSeranade() As Integer
        Get
            Return startRowSerenade
        End Get
        Set(ByVal value As Integer)
            startRowSerenade = value
        End Set
    End Property
    Public Property EndRecSerenade() As Integer
        Get
            Return endrowSerenade
        End Get
        Set(ByVal value As Integer)
            endrowSerenade = value
        End Set
    End Property
    Public Property TotalServiceCount() As Integer
        Get
            Return iTotalServiceCount
        End Get
        Set(ByVal value As Integer)
            iTotalServiceCount = value
        End Set
    End Property


End Class
