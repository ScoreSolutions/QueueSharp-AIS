Public Class frmForce

    Dim dt_data As New DataTable

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If dtpEnd.Value.Date < dtpStart.Value.Date Or dtpStart.Value.Date < FindDateNow.Date Then
            MessageBox.Show("Your Date Range is incorrect." & vbCrLf & "Please reselect the Date Range", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim chk As Boolean = False
        For i As Int32 = 0 To flpTime.Controls.Count - 1
            If flpTime.Controls(i).BackColor = Color.ForestGreen Then
                chk = True
                Exit For
            End If
        Next
        'If chk = False Then
        '    MessageBox.Show("Please Select Time Slot", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        Dim sql As String = ""
        Dim dt As New DataTable

        Dim dtSlot As New DataTable
        dtSlot.Columns.Add("ST", GetType(System.String))
        dtSlot.Columns.Add("ET", GetType(System.String))
        Dim StartSlot As String = ""
        Dim EndSlot As String = ""
        Dim CheckDate As Boolean = False
        For i As Int32 = 0 To flpTime.Controls.Count - 1
            If flpTime.Controls(i).BackColor = Color.ForestGreen Then
                If StartSlot = "" Then
                    StartSlot = flpTime.Controls(i).Text
                ElseIf StartSlot <> "" Then
                    EndSlot = flpTime.Controls(i).Text
                End If
                If i = flpTime.Controls.Count - 1 Then
                    Dim dr As DataRow = dtSlot.NewRow
                    dr("ST") = StartSlot
                    dr("ET") = flpTime.Controls(i).Text
                    dtSlot.Rows.Add(dr)
                End If
            Else
                If StartSlot <> "" And EndSlot <> "" Then
                    Dim dr As DataRow = dtSlot.NewRow
                    dr("ST") = StartSlot
                    dr("ET") = EndSlot
                    dtSlot.Rows.Add(dr)
                    StartSlot = ""
                    EndSlot = ""
                ElseIf StartSlot <> "" And EndSlot = "" Then
                    Dim dr As DataRow = dtSlot.NewRow
                    dr("ST") = StartSlot
                    dr("ET") = StartSlot
                    dtSlot.Rows.Add(dr)
                    StartSlot = ""
                    EndSlot = ""
                End If
            End If
        Next

        Dim ST_Date As New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbMon.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000

                Do While ST_Date.DayOfWeek <> DayOfWeek.Monday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next

            
        End If

        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbTue.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Tuesday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If
        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbWed.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Wednesday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If
        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbThu.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Thursday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If
        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbFri.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Friday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If

        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbSat.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Saturday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If
        ST_Date = New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day)
        If cbSun.Checked Then
            CheckDate = True
            For i As Int32 = 0 To 1000
                Do While ST_Date.DayOfWeek <> DayOfWeek.Sunday
                    ST_Date = ST_Date.AddDays(1)
                Loop

                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If


        If CheckDate = False Then
            For i As Int32 = 0 To 1000
                If ST_Date <= dtpEnd.Value Then
                    sql = "delete from TB_FORCE_SCHEDULE where CONVERT(varchar(8),force_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_FORCE_SCHEDULE")
                        sql = "insert into TB_FORCE_SCHEDULE(id,force_date,slot,wait,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudWait.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                Else
                    Exit For
                End If
                ST_Date = ST_Date.AddDays(1)
            Next
        End If

        cbSlot.SelectedIndex = 0
        dtpStart.Value = FindDateNow()
        Dim DayInMonth As New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, Date.DaysInMonth(dtpStart.Value.Year, dtpStart.Value.Month))
        dtpEnd.Value = DayInMonth
        ClearCheckbox()
        GenSlot()
        MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        SetDateEvent()
    End Sub

    Sub ClearCheckbox()
        cbMon.Checked = False
        cbTue.Checked = False
        cbWed.Checked = False
        cbThu.Checked = False
        cbFri.Checked = False
        cbSat.Checked = False
        cbSun.Checked = False
    End Sub

    Sub AddSlotTime(ByVal RowID As String, ByVal Time As String)
        Dim lbl As New Label
        With lbl
            .AutoSize = False
            .Width = 80
            .Height = 35
            .Name = RowID
            .Text = Time
            .BackColor = Color.LightGray
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
        End With
        flpTime.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf CheckStatus
    End Sub

    Private Sub CheckStatus(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Label = Sender
        If btn.BackColor = Color.LightGray Then
            btn.BackColor = Color.ForestGreen
            btn.ForeColor = Color.White
        Else
            btn.BackColor = Color.LightGray
            btn.ForeColor = Color.Black
        End If
    End Sub

    Private Sub btnGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGen.Click
        GenSlot()
    End Sub

    Sub GenSlot()
        If cbSlot.Text.Trim <> "" Then
            flpTime.Controls.Clear()
            Dim sql As String = ""
            Dim dt As New DataTable
            Dim slot As Int32 = CInt(cbSlot.Text)

            Dim StartTime As Date = CDate(CBstart_time_h.Text & ":" & CBstart_time_m.Text)
            Dim EndTime As Date = CDate(CBend_time_h.Text & ":" & CBend_time_m.Text)
            Dim SlotTime As Date = StartTime
            Dim ret As Boolean = False
            For i As Int32 = 0 To 1000
                If SlotTime < EndTime Then
                    AddSlotTime(SlotTime.Hour.ToString.PadLeft(2, "0") & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"), ret)
                ElseIf SlotTime = EndTime Or (SlotTime > EndTime And SlotTime <> EndTime) Then
                    AddSlotTime(EndTime.Hour.ToString.PadLeft(2, "0") & ":" & EndTime.Minute.ToString.PadLeft(2, "0"), ret)
                    Exit For
                Else
                    Exit For
                End If
                SlotTime = DateAdd(DateInterval.Minute, slot, SlotTime)
                ret = False
            Next
        End If
    End Sub

    Sub AddSlotTime(ByVal Time As String, ByVal Status As Boolean)
        Dim lbl As New Label
        With lbl
            .AutoSize = False
            .Width = 80
            .Height = 35
            .Name = Time
            .Text = Time
            If Status = True Then
                .BackColor = Color.ForestGreen
                .ForeColor = Color.White
            Else
                .BackColor = Color.LightGray
                .ForeColor = Color.Black
            End If
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
        End With
        flpTime.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf CheckStatus
    End Sub

    Private Sub frmAppointment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DayInMonth As New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, Date.DaysInMonth(dtpStart.Value.Year, dtpStart.Value.Month))
        dtpEnd.Value = DayInMonth
        SetDateEvent()
        AppointPolicies()
        GenSlot()
    End Sub

    Sub AppointPolicies()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from TB_SETTING"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Try
                    Select Case dt.Rows(i).Item("config_name").ToString.Trim
                        Case "s_open"
                            Dim Time(1) As String
                            Time = dt.Rows(i).Item("config_value").ToString.Split(":")
                            CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(Time(0).ToString.PadLeft(2, "0"))
                            CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(Time(1).ToString.PadLeft(2, "0"))
                            lblShopOpen.Text = dt.Rows(i).Item("config_value").ToString
                        Case "s_close"
                            Dim Time(1) As String
                            Time = dt.Rows(i).Item("config_value").ToString.Split(":")
                            CBend_time_h.SelectedIndex = CBend_time_h.FindString(Time(0).ToString.PadLeft(2, "0"))
                            CBend_time_m.SelectedIndex = CBend_time_m.FindString(Time(1).ToString.PadLeft(2, "0"))
                            lblShopOpen.Text += " - " & dt.Rows(i).Item("config_value").ToString
                        Case "s_slot"
                            cbSlot.SelectedIndex = cbSlot.FindString(dt.Rows(i).Item("config_value").ToString)
                    End Select
                Catch ex As Exception : End Try
            Next
        End If
    End Sub

    Private Sub SetDateEvent()
        Dim dt As New DataTable
        Dim sql As String = "select distinct force_date from TB_FORCE_SCHEDULE"
        dt = getDataTable(sql)
        mc.SetEventDates(dt, "force_date")
    End Sub

    Private Sub mc_DateChange(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles mc.DateChange
        Dim sql As String = ""
        Dim dt As New DataTable

        sql = "select min(start_slot) as ST,max(end_slot) as ET,slot,wait from TB_FORCE_SCHEDULE where convert(varchar(8),force_date,112) = '" & FixDate(mc.StartDate) & "' group by slot,wait"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(CDate(dt.Rows(0).Item("ST")).Hour.ToString.PadLeft(2, "0"))
            CBstart_time_m.SelectedIndex = CBstart_time_h.FindString(CDate(dt.Rows(0).Item("ST")).Minute.ToString.PadLeft(2, "0"))
            CBend_time_h.SelectedIndex = CBend_time_h.FindString(CDate(dt.Rows(0).Item("ET")).Hour.ToString.PadLeft(2, "0"))
            CBend_time_m.SelectedIndex = CBend_time_m.FindString(CDate(dt.Rows(0).Item("ET")).Minute.ToString.PadLeft(2, "0"))
            cbSlot.SelectedIndex = cbSlot.FindString(dt.Rows(0).Item("slot"))
            GenSlot()
            nudWait.Value = dt.Rows(0).Item("wait")

            dt = New DataTable
            sql = "select * from TB_FORCE_SCHEDULE where convert(varchar(8),force_date,112) = '" & FixDate(mc.StartDate) & "'"
            dt = getDataTable(sql)
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim Slot As Int32 = dt.Rows(i).Item("slot").ToString
                Dim StartTime As Date = dt.Rows(i).Item("start_time").ToString
                Dim EndTime As Date = dt.Rows(i).Item("end_time").ToString

                For x As Int32 = 0 To flpTime.Controls.Count - 1
                    If StartTime <= EndTime Then
                        If flpTime.Controls(x).Text = StartTime.Hour.ToString.PadLeft(2, "0") & ":" & StartTime.Minute.ToString.PadLeft(2, "0") Then
                            flpTime.Controls(x).BackColor = Color.ForestGreen
                            flpTime.Controls(x).ForeColor = Color.White
                            StartTime = DateAdd(DateInterval.Minute, Slot, StartTime)
                        End If
                    End If

                Next
            Next

        Else
            dt = New DataTable
            sql = "select config_name,config_value from TB_SETTING where config_name in ('s_open','s_close')"
            dt = getDataTable(sql)
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim time(1) As String
                If dt.Rows(i).Item("config_name").ToString = "s_open" Then
                    time = dt.Rows(i).Item("config_value").ToString.Split(":")
                    CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(time(0).ToString.PadLeft(2, "0"))
                    CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(time(1).ToString.PadLeft(2, "0"))
                Else
                    time = dt.Rows(i).Item("config_value").ToString.Split(":")
                    CBend_time_h.SelectedIndex = CBend_time_h.FindString(time(0).ToString.PadLeft(2, "0"))
                    CBend_time_m.SelectedIndex = CBend_time_m.FindString(time(1).ToString.PadLeft(2, "0"))
                End If

            Next
            GenSlot()
        End If
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        If p_date.Enabled = True Then
            btnView.Text = "Edit Slot(s)"
            p_date.Enabled = False
            p_capa.Enabled = False
            p_service.Enabled = False
            p_week.Enabled = False
            p_slot.Enabled = False
            btnSave.Enabled = False
            mc.Enabled = True
            mc.DateValue = FindDateNow()
            mc.StartDate = FindDateNow()
            mc.EndDate = FindDateNow()
            mc_DateChange(Nothing, Nothing)
        Else
            btnView.Text = "View Slot(s) by Date"
            p_date.Enabled = True
            p_capa.Enabled = True
            p_service.Enabled = True
            p_week.Enabled = True
            p_slot.Enabled = True
            btnGen.Enabled = True
            btnSave.Enabled = True
            'mc.DateValue = FindDateNow()
            'mc.StartDate = FindDateNow()
            'mc.EndDate = FindDateNow()
            mc.Enabled = False
        End If
    End Sub

    Sub CheckDayOfWeek()
        lblDayOfWeek.Text = ""

        If cbMon.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbMon.Text & ", "
        End If

        If cbTue.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbTue.Text & ", "
        End If

        If cbWed.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbWed.Text & ", "
        End If

        If cbThu.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbThu.Text & ", "
        End If

        If cbFri.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbFri.Text & ", "
        End If

        If cbSat.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbSat.Text & ", "
        End If

        If cbSun.Checked Then
            lblDayOfWeek.Text = lblDayOfWeek.Text & cbSun.Text & ", "
        End If

        If lblDayOfWeek.Text <> "" Then
            lblDayOfWeek.Text = StringFromLeft(lblDayOfWeek.Text, lblDayOfWeek.Text.Length - 2)
        End If
    End Sub

    Private Sub cb_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSun.CheckedChanged, cbMon.CheckedChanged, cbTue.CheckedChanged, cbWed.CheckedChanged, cbThu.CheckedChanged, cbFri.CheckedChanged, cbSat.CheckedChanged
        CheckDayOfWeek()
    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        If dtpStart.Value.Date = dtpEnd.Value.Date Then
            lblRecur.Text = dtpStart.Value.Date.ToShortDateString
        Else
            lblRecur.Text = dtpStart.Value.Date.ToShortDateString & " - " & dtpEnd.Value.Date.ToShortDateString
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim config_name As String = ""
        Dim config_value As String = ""
        Dim config_desc As String = ""
        Dim sql As String = ""
        'เวลาเปิด Shop
        config_name = "s_open"
        config_value = CBstart_time_h.Text & ":" & CBstart_time_m.Text
        config_desc = "(Shop) Open"
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'เวลาปิด Shop
        config_name = "s_close"
        config_value = CBend_time_h.Text & ":" & CBend_time_m.Text
        config_desc = "(Shop) Close"
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

    End Sub

    Private Sub CBend_time_h_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBstart_time_h.SelectedIndexChanged, CBstart_time_m.SelectedIndexChanged, CBend_time_h.SelectedIndexChanged, CBend_time_m.SelectedIndexChanged
        lblShopOpen.Text = CBstart_time_h.Text & ":" & CBstart_time_m.Text & "   To   " & CBend_time_h.Text & ":" & CBend_time_m.Text
    End Sub

End Class