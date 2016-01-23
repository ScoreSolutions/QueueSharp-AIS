Imports Engine.Common.ShopConnectDBENG
Public Class frmAppointment

    Dim dt_data As New DataTable

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If dtpEnd.Value.Date < dtpStart.Value.Date Or dtpStart.Value.Date < FindDateNow.Date Then
            MessageBox.Show("Your Date Range is incorrect." & vbCrLf & "Please reselect the Date Range", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim chkService As Boolean = False
        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            If FLP_Item.Controls(i).BackColor = Color.ForestGreen Then
                chkService = True
                Exit For
            End If
        Next

        Dim chkSlot As Boolean = False
        For i As Int32 = 0 To flpTime.Controls.Count - 1
            If flpTime.Controls(i).BackColor = Color.ForestGreen Then
                chkSlot = True
                Exit For
            End If
        Next
        If chkService = True And chkSlot = False Then
            MessageBox.Show("Please Select Time Slot", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf chkService = False And chkSlot = True Then
            MessageBox.Show("Please Select Service.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim sql As String = ""
        Dim dt As New DataTable

        sql = "select * from TB_APPOINTMENT_CUSTOMER where CONVERT(varchar(8),app_date,112)  between '" & FixDate(dtpStart.Value) & "' and '" & FixDate(dtpEnd.Value) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Dim App_Time As String = ""
            For i As Int32 = 0 To dt.Rows.Count - 1
                App_Time = "" & vbCrLf
            Next
            MessageBox.Show("Cannot Set Appointment " & vbCrLf & "Because customer has booked", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtpStart.Value.DayOfWeek <> DayOfWeek.Monday And dtpStart.Value.DayOfWeek <> DayOfWeek.Sunday Then
            sql = "select distinct slot From TB_APPOINTMENT_SCHEDULE where app_date  between (select dateadd(d,2-datepart(dw,'" & _
                  FixDate(dtpStart.Value) & "'),'" & FixDate(dtpStart.Value) & "')) and (select dateadd(d,datepart(dw,'" & _
                  FixDate(dtpStart.Value) & "'),'" & FixDate(dtpStart.Value) & "'))"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                If cbSlot.Text <> dt.Rows(0).Item(0) Then
                    MessageBox.Show("Cannot Set Appointment " & vbCrLf & "Because slot time can be set " & dt.Rows(0).Item(0) & " Mins in this week", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

        ElseIf dtpStart.Value.DayOfWeek = DayOfWeek.Sunday Then
            Dim dDate As Date = dtpStart.Value
            Dim strDate As String = dDate.Day - 1 & "/" & dDate.Month & "/" & dDate.Year
            sql = "select distinct slot From TB_APPOINTMENT_SCHEDULE where app_date  between (select dateadd(d,2-datepart(dw,'" & _
                  FixDate(strDate) & "'),'" & FixDate(strDate) & "')) and (select dateadd(d,datepart(dw,'" & _
                  FixDate(strDate) & "'),'" & FixDate(strDate) & "'))"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                If cbSlot.Text <> dt.Rows(0).Item(0) Then
                    MessageBox.Show("Cannot Set Appointment " & vbCrLf & "Because slot time can be set " & dt.Rows(0).Item(0) & " Mins in this week", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
        End If

        dt = New DataTable
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
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
                    sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                    executeSQL(sql)
                    For x As Int32 = 0 To dtSlot.Rows.Count - 1
                        Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                        sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                        executeSQL(sql)
                    Next
                    AddServiceAppointment(ST_Date)
                    AddSlot(nudCounter.Value, ST_Date)
                    ST_Date = ST_Date.AddDays(1)
                Else
                    Exit For
                End If
            Next
        End If


        If CheckDate = False Then
            Do
                sql = "delete from TB_APPOINTMENT_SCHEDULE where CONVERT(varchar(8),app_date,112)  = '" & FixDate(ST_Date) & "'"
                executeSQL(sql)
                For x As Int32 = 0 To dtSlot.Rows.Count - 1
                    Dim ID As Int32 = FindID("TB_APPOINTMENT_SCHEDULE")
                    sql = "insert into TB_APPOINTMENT_SCHEDULE(id,app_date,slot,gap,capacity,start_time,end_time,create_by,create_date,start_slot,end_slot) values(" & ID & ",'" & FixDate(ST_Date) & "'," & cbSlot.Text & "," & nudGap.Value & "," & nudCounter.Value & ",'" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ST").ToString & "','" & FixDate(ST_Date) & " " & dtSlot.Rows(x).Item("ET").ToString & "'," & myUser.user_id & ",getdate(),'" & FixDate(ST_Date) & " " & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "','" & FixDate(ST_Date) & " " & CBend_time_h.Text & ":" & CBend_time_m.Text & "')"
                    executeSQL(sql)
                Next
                AddServiceAppointment(ST_Date)
                AddSlot(nudCounter.Value, ST_Date)

                ST_Date = ST_Date.AddDays(1)
            Loop While ST_Date <= dtpEnd.Value
        End If

        'sql = "update TB_SETTING set config_value = '" & CBstart_time_h.Text & ":" & CBstart_time_m.Text & "' where config_name = 's_open'"
        'executeSQL(sql)
        'sql = "update TB_SETTING set config_value = '" & CBend_time_h.Text & ":" & CBend_time_m.Text & "' where config_name = 's_close'"
        'executeSQL(sql)
        sql = "update TB_SETTING set config_value = '" & nud_k_no_show.Value & "' where config_name = 'k_no_show'"
        executeSQL(sql)
        sql = "update TB_SETTING set config_value = '" & nud_k_not_book.Value & "' where config_name = 'k_not_booked'"
        executeSQL(sql)

        sql = "update TB_SETTING set config_value = '" & nud_k_no_show_within.Value & "' where config_name = 'nud_k_no_show_within'"
        executeSQL(sql)

        cbSlot.SelectedIndex = 0
        dtpStart.Value = FindDateNow()
        Dim DayInMonth As New DateTime(dtpStart.Value.Year, dtpStart.Value.Month, Date.DaysInMonth(dtpStart.Value.Year, dtpStart.Value.Month))
        dtpEnd.Value = DayInMonth
        ClearCheckbox()
        GenSlot()

        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            FLP_Item.Controls(i).BackColor = Color.White
            FLP_Item.Controls(i).ForeColor = Color.Black
        Next

        MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        SetDateEvent()
        ''************* Config ***************
        'sql = "select * from TB_APPOINTMENT"
        'dt = getDataTable(sql)
        'If dt.Rows.Count > 0 Then
        '    sql = "update TB_APPOINTMENT set before_time = " & NUDbefore_time.Value & ",late_time = " & NUDlate_time.Value & ",before_app_time = " & NUDbefore_app_time.Value & ",start_time_h = " & CInt(CBstart_time_h.Text) & ",start_time_m = " & CInt(CBstart_time_m.Text) & ",end_time_h = " & CInt(CBend_time_h.Text) & ",end_time_m = " & CInt(CBend_time_m.Text) & ",slot = " & NUDslot.Value & ",update_by = " & myUser.user_id & ",update_date = getdate()"
        'Else
        '    sql = "insert into TB_APPOINTMENT(before_time,late_time,before_app_time,start_time_h,start_time_m,end_time_h,end_time_m,slot,update_by,update_date) values(" & NUDbefore_time.Value & "," & NUDlate_time.Value & "," & NUDbefore_app_time.Value & "," & CInt(CBstart_time_h.Text) & "," & CInt(CBstart_time_m.Text) & "," & CInt(CBend_time_h.Text) & "," & CInt(CBend_time_m.Text) & "," & NUDslot.Value & "," & myUser.user_id & ",getdate())"
        'End If
        'executeSQL(sql)
        ''*************** Slot ***************
        'sql = "Delete from TB_SLOT"
        'executeSQL(sql)
        'Dim StartSlot As String = ""
        'Dim EndSlot As String = ""
        'Dim ID As Int32 = 0

        'For i As Int32 = 0 To flpTime.Controls.Count - 1
        '    If flpTime.Controls(i).BackColor = Color.DarkGreen Then
        '        If StartSlot = "" Then
        '            StartSlot = flpTime.Controls(i).Text
        '        ElseIf StartSlot <> "" Then
        '            EndSlot = flpTime.Controls(i).Text
        '        End If
        '    Else
        '        If StartSlot <> "" And EndSlot <> "" Then
        '            ID = FindID("TB_SLOT")
        '            sql = "insert into TB_SLOT(id,start_time,end_time,update_by,update_date) values(" & ID & ",'" & StartSlot & "','" & EndSlot & "'," & myUser.user_id & ",getdate())"
        '            executeSQL(sql)
        '        End If
        '        StartSlot = ""
        '        EndSlot = ""
        '    End If
        'Next
        ''************************************
        'MessageBox.Show("Success !!!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub frmAppointment_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    'Private Sub frmAppointment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    'Dim sql As String = ""
    'sql = "Select * from TB_APPOINTMENT"
    'dt_data = getDataTable(sql)
    'If dt_data.Rows.Count > 0 Then
    '    NUDbefore_time.Value = dt_data.Rows(0).Item("before_time").ToString
    '    NUDlate_time.Value = dt_data.Rows(0).Item("late_time").ToString
    '    NUDbefore_app_time.Value = dt_data.Rows(0).Item("before_app_time").ToString
    '    CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(StringFromRight(("0" & dt_data.Rows(0).Item("start_time_h").ToString), 2))
    '    CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(StringFromRight(("0" & dt_data.Rows(0).Item("start_time_m").ToString), 2))
    '    CBend_time_h.SelectedIndex = CBend_time_h.FindString(StringFromRight(("0" & dt_data.Rows(0).Item("end_time_h").ToString), 2))
    '    CBend_time_m.SelectedIndex = CBend_time_m.FindString(StringFromRight(("0" & dt_data.Rows(0).Item("end_time_m").ToString), 2))
    '    NUDslot.Value = dt_data.Rows(0).Item("slot").ToString

    '    GenSlot(dt_data.Rows(0).Item("start_time_h"), dt_data.Rows(0).Item("start_time_m"), dt_data.Rows(0).Item("end_time_h"), dt_data.Rows(0).Item("end_time_m"))

    '    FindDataSlot()
    'End If
    'End Sub

    'Sub FindDataSlot()
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "select * from TB_SLOT"
    '    dt = getDataTable(sql)
    '    If dt.Rows.Count > 0 Then
    '        For i As Int32 = 0 To dt.Rows.Count - 1
    '            For j As Int32 = 0 To flpTime.Controls.Count - 1
    '                If (CDate(flpTime.Controls(j).Text) >= CDate(dt.Rows(i).Item("start_time"))) And (CDate(flpTime.Controls(j).Text) <= CDate(dt.Rows(i).Item("end_time"))) Then
    '                    flpTime.Controls(j).BackColor = Color.DarkGreen
    '                    flpTime.Controls(j).ForeColor = Color.White
    '                End If
    '            Next
    '        Next
    '    End If
    'End Sub

    Private Sub btnGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGen.Click
        Dim ShopOpen As String = CBstart_time_h.Text & ":" & CBstart_time_m.Text
        Dim ShopClose As String = CBend_time_h.Text & ":" & CBend_time_m.Text
        If ShopOpen < GetShopConfig(INIFileName, "s_open", ApplicationName, "frmAppointment_btnGen.Click") Then
            MessageBox.Show("Cannot Genarate Time Slot " & vbCrLf & "Because Start Time is Before Shop Open Time", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ShopClose > GetShopConfig(INIFileName, "s_close", ApplicationName, "frmAppointment_btnGen.Click") Then
            MessageBox.Show("Cannot Genarate Time Slot " & vbCrLf & "Because End Time is After Shop Close Time", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If dtpStart.Value.DayOfWeek <> DayOfWeek.Monday Then
            MessageBox.Show("Cannot Genarate Time Slot " & vbCrLf & "Because Date Range is Start on Monday", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtpEnd.Value.DayOfWeek <> DayOfWeek.Sunday Then
            MessageBox.Show("Cannot Genarate Time Slot " & vbCrLf & "Because Date Range is Finish on Sunday", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        GenSlot()
    End Sub

    Sub GenSlot()
        flpTime.Controls.Clear()
        Dim sql As String = ""
        Dim dt As New DataTable
        'Dim dt_time As New DataTable
        Dim slot As Int32 = CInt(cbSlot.Text)

        Dim StartTime As Date = CDate(CBstart_time_h.Text & ":" & CBstart_time_m.Text)
        'Dim EndTime As Date = DateAdd(DateInterval.Minute, (CInt(cbCloseShop.Text) + CInt(cbSlot.Text)) * -1, CDate(CBend_time_h.Text & ":" & CBend_time_m.Text))
        'Dim EndTime As Date = DateAdd(DateInterval.Minute, (CInt(cbCloseShop.Text) + CInt(cbSlot.Text)) * -1, CDate(GetShopConfig(INIFileName, "s_close", ApplicationName, "frmAppointment_GenSlot")))
        Dim EndTime As Date = CDate(CBend_time_h.Text & ":" & CBend_time_m.Text)
        Dim ShopCloseTime As Date = DateAdd(DateInterval.Minute, (CInt(cbCloseShop.Text) + CInt(cbSlot.Text)) * -1, CDate(GetShopConfig(INIFileName, "s_close", ApplicationName, "frmAppointment_GenSlot")))
        If EndTime > ShopCloseTime Then
            EndTime = ShopCloseTime
        End If
        Dim SlotTime As Date = StartTime
        Dim ret As Boolean = False
        Do
            If SlotTime < EndTime Then
                AddSlotTime(SlotTime.Hour.ToString.PadLeft(2, "0") & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"), ret)
            ElseIf SlotTime = EndTime Or (SlotTime > EndTime And SlotTime <> EndTime) Then
                AddSlotTime(EndTime.Hour.ToString.PadLeft(2, "0") & ":" & EndTime.Minute.ToString.PadLeft(2, "0"), ret)
                Exit Do
            Else
                Exit Do
            End If
            SlotTime = DateAdd(DateInterval.Minute, slot, SlotTime)
            'ret = False
        Loop While SlotTime.ToString("HH:mm") <= EndTime.ToString("HH:mm")
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

    'Sub GenSlot(ByVal StartTime_h As Int32, ByVal StartTime_m As Int32, ByVal EndTime_h As Int32, ByVal EndTime_m As Int32)
    '    flpTime.Controls.Clear()
    '    Dim Start As String = StringFromRight(("0" & StartTime_h.ToString), 2) & ":" & StringFromRight(("0" & StartTime_m.ToString), 2)
    '    Dim End_ As String = StringFromRight(("0" & EndTime_h.ToString), 2) & ":" & StringFromRight(("0" & EndTime_m.ToString), 2)
    '    Dim StartTime As Date = CDate(Start)
    '    Dim EndTime As Date = CDate(End_)
    '    Dim SlotTime As Date = StartTime
    '    For i As Int32 = 0 To 100
    '        If i = 0 Then
    '            AddSlotTime(i, SlotTime.Hour.ToString & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"))
    '        Else
    '            If SlotTime < EndTime Then
    '                AddSlotTime(i, SlotTime.Hour.ToString & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"))
    '            ElseIf SlotTime = EndTime Or (SlotTime > EndTime And SlotTime <> EndTime) Then
    '                AddSlotTime(i, EndTime.Hour.ToString & ":" & EndTime.Minute.ToString.PadLeft(2, "0"))
    '                Exit For
    '            Else
    '                Exit For
    '            End If
    '        End If
    '        SlotTime = DateAdd(DateInterval.Minute, CInt(dt_data.Rows(0).Item("slot").ToString), SlotTime)
    '    Next
    'End Sub

    Private Sub frmAppointment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        Dim sql As String
        dt = New DataTable
        sql = "select TB_ITEM.id,TB_ITEM.item_name  from TB_SKILL_ITEM left join TB_SKILL on TB_SKILL_ITEM.skill_id = TB_SKILL.id left join TB_ITEM on TB_SKILL_ITEM.item_id = TB_ITEM.id where TB_SKILL.active_status = 1 and TB_SKILL.appointment = 1 and TB_ITEM.active_status = 1"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                AddService(dt.Rows(i).Item("item_name").ToString, dt.Rows(i).Item("id").ToString)
            Next
        End If

        cbSlot.SelectedIndex = 0
        dtpStart.Value = FindDateNow()
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
                        'Case "k_no_show" 'No Show
                        '    nud_k_no_show.Value = dt.Rows(i).Item("config_value").ToString
                        'Case "k_not_booked" 'Not Booked
                        '    nud_k_not_book.Value = dt.Rows(i).Item("config_value").ToString
                        Case "s_open"
                            Dim Time(1) As String
                            Time = dt.Rows(i).Item("config_value").ToString.Split(":")
                            CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(Time(0).ToString.PadLeft(2, "0"))
                            CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(Time(1).ToString.PadLeft(2, "0"))
                        Case "s_close"
                            Dim Time(1) As String
                            Time = dt.Rows(i).Item("config_value").ToString.Split(":")
                            CBend_time_h.SelectedIndex = CBend_time_h.FindString(Time(0).ToString.PadLeft(2, "0"))
                            CBend_time_m.SelectedIndex = CBend_time_m.FindString(Time(1).ToString.PadLeft(2, "0"))

                        Case "s_slot"
                            cbSlot.SelectedIndex = cbSlot.FindString(dt.Rows(i).Item("config_value").ToString)
                        Case "k_before_close"
                            cbCloseShop.SelectedIndex = cbCloseShop.FindString(dt.Rows(i).Item("config_value").ToString)
                            'Case "nud_k_no_show_within"
                            '    nud_k_no_show_within.Value = dt.Rows(i).Item("config_value").ToString
                    End Select
                Catch ex As Exception : End Try
            Next
        End If

        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True)
            Dim txtPolicy As String = ws.GetAppointmentPolicy
            Dim tmp() As String = Split(txtPolicy, "#")
            If tmp.Length = 3 Then
                nud_k_no_show.Value = tmp(0)
                nud_k_no_show_within.Value = tmp(1)
                nud_k_not_book.Value = tmp(2)
            End If
            ws = Nothing
        Catch ex As Exception
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Url = GetWebServiceURL(False)
            Dim txtPolicy As String = ws.GetAppointmentPolicy
            Dim tmp() As String = Split(txtPolicy, "#")
            If tmp.Length = 3 Then
                nud_k_no_show.Value = tmp(0)
                nud_k_no_show_within.Value = tmp(1)
                nud_k_not_book.Value = tmp(2)
            End If
            ws = Nothing
        End Try
    End Sub

    Sub AddService(ByVal ItemName As String, ByVal ItemId As Int32)
        Dim lbl As New Label
        'Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        With lbl
            .AutoSize = False
            .Width = 205
            .Height = 30
            .Tag = ItemId.ToString
            .Name = "lbl" & "_" & ItemId.ToString
            .Text = ItemName
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.White
            '.Font = Font
        End With
        FLP_Item.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf CheckStatusItem
    End Sub

    Private Sub CheckStatusItem(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Label = Sender
        If btn.BackColor = Color.White Then
            btn.BackColor = Color.ForestGreen
            btn.ForeColor = Color.White
        Else
            btn.BackColor = Color.White
            btn.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SetDateEvent()

        Dim dt As New DataTable

        Dim sql As String = "select distinct app_date from TB_APPOINTMENT_SCHEDULE"
        dt = getDataTable(sql)
        mc.SetEventDates(dt, "app_date")

    End Sub

    Private Sub mc_DateChange(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles mc.DateChange
        Dim sql As String = ""
        Dim dt As New DataTable

        'If mc.StartDate.Date < FindDateNow.Date Then
        '    flpTime.Controls.Clear()
        '    Exit Sub
        'End If

        If FLP_Item.Controls.Count = 0 Then
            Exit Sub
        End If

        '******************** Item **********************
        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            FLP_Item.Controls(i).BackColor = Color.White
            FLP_Item.Controls(i).ForeColor = Color.Black
        Next

        dt = New DataTable
        sql = "select * from TB_APPOINTMENT_ITEM where convert(varchar(8),app_date,112) = '" & FixDate(mc.StartDate) & "'"
        dt = getDataTable(sql)
        For i As Int32 = 0 To dt.Rows.Count - 1
            Try
                FLP_Item.Controls("lbl_" & dt.Rows(i).Item("item_id").ToString).BackColor = Color.ForestGreen
                FLP_Item.Controls("lbl_" & dt.Rows(i).Item("item_id").ToString).ForeColor = Color.White
            Catch ex As Exception
            End Try
        Next
        '************************************************


        'If mc.EndDate.Date > mc.StartDate.Date Then
        '    sql = "select app_date from TB_APPOINTMENT_CUSTOMER where CONVERT(varchar(8),app_date,112)  between '" & FixDate(dtpStart.Value) & "' and '" & FixDate(dtpEnd.Value) & "'"
        '    dt = getDataTable(sql)
        '    If dt.Rows.Count > 0 Then
        '        MessageBox.Show("Cannot Set Appointment. " & vbCrLf & "Because customer has booked" & vbCrLf & CDate(dt.Rows(0).Item("app_date")).ToShortDateString, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Exit Sub
        '    End If
        'End If


        sql = "select min(start_slot) as ST,max(end_slot) as ET,slot,gap,capacity from TB_APPOINTMENT_SCHEDULE where convert(varchar(8),app_date,112) = '" & FixDate(mc.StartDate) & "' group by slot,gap,capacity"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(CDate(dt.Rows(0).Item("ST")).Hour.ToString.PadLeft(2, "0"))
            CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(CDate(dt.Rows(0).Item("ST")).Minute.ToString.PadLeft(2, "0"))
            CBend_time_h.SelectedIndex = CBend_time_h.FindString(CDate(dt.Rows(0).Item("ET")).Hour.ToString.PadLeft(2, "0"))
            CBend_time_m.SelectedIndex = CBend_time_m.FindString(CDate(dt.Rows(0).Item("ET")).Minute.ToString.PadLeft(2, "0"))
            nudGap.Value = dt.Rows(0).Item("gap")
            cbSlot.SelectedIndex = cbSlot.FindString(dt.Rows(0).Item("slot"))
            GenSlot()
            nudCounter.Value = dt.Rows(0).Item("capacity")

            dt = New DataTable
            sql = "select * from TB_APPOINTMENT_SCHEDULE where convert(varchar(8),app_date,112) = '" & FixDate(mc.StartDate) & "'"
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

    Sub AddServiceAppointment(ByVal DateSelect As Date)
        Dim sql As String = ""
        sql = "delete from TB_APPOINTMENT_ITEM where convert(varchar(8),app_date,112) = '" & FixDate(DateSelect) & "'"
        executeSQL(sql)

        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            If FLP_Item.Controls(i).BackColor = Color.ForestGreen Then
                Dim Id As Int32 = FindID("TB_APPOINTMENT_ITEM")
                sql = "insert into TB_APPOINTMENT_ITEM(id,app_date,item_id,create_by,create_date) values(" & Id & ",'" & FixDate(DateSelect) & "'," & FLP_Item.Controls(i).Tag & "," & myUser.user_id & ",getdate())"
                executeSQL(sql)
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
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
            'Dim dt As New DataTable
            'Dim sql As String = ""
            'sql = "select config_name,config_value from TB_SETTING where config_name in ('s_open','s_close')"
            'dt = getDataTable(sql)
            'For i As Int32 = 0 To dt.Rows.Count - 1
            '    Dim time(1) As String
            '    If dt.Rows(i).Item("config_name").ToString = "s_open" Then
            '        time = dt.Rows(i).Item("config_value").ToString.Split(":")
            '        CBstart_time_h.SelectedIndex = CBstart_time_h.FindString(time(0).ToString.PadLeft(2, "0"))
            '        CBstart_time_m.SelectedIndex = CBstart_time_m.FindString(time(1).ToString.PadLeft(2, "0"))
            '    Else
            '        time = dt.Rows(i).Item("config_value").ToString.Split(":")
            '        CBend_time_h.SelectedIndex = CBend_time_h.FindString(time(0).ToString.PadLeft(2, "0"))
            '        CBend_time_m.SelectedIndex = CBend_time_m.FindString(time(1).ToString.PadLeft(2, "0"))
            '    End If
            'Next
            'For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            '    FLP_Item.Controls(i).BackColor = Color.White
            '    FLP_Item.Controls(i).ForeColor = Color.Black
            'Next
            'flpTime.Controls.Clear()
            'GenSlot()
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

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim config_name As String = ""
        Dim config_value As String = ""
        Dim sql As String = ""
        'เวลาเปิด Shop
        config_name = "s_open"
        config_value = CBstart_time_h.Text & ":" & CBstart_time_m.Text
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'เวลาปิด Shop
        config_name = "s_close"
        config_value = CBend_time_h.Text & ":" & CBend_time_m.Text
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'With in
        config_name = "nud_k_no_show_within"
        config_value = nud_k_no_show_within.Value
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'No Show
        config_name = "k_no_show"
        config_value = nud_k_no_show.Value.ToString
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'Not Booked
        config_name = "k_not_booked"
        config_value = nud_k_not_book.Value.ToString
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)

        'Before Cloae
        config_name = "k_before_close"
        config_value = cbCloseShop.Text
        sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        executeSQL(sql)


        MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'Private Sub CBend_time_h_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBstart_time_h.SelectedIndexChanged, CBstart_time_m.SelectedIndexChanged, CBend_time_h.SelectedIndexChanged, CBend_time_m.SelectedIndexChanged
    '    lblShopOpen.Text = CBstart_time_h.Text & ":" & CBstart_time_m.Text & "   To   " & CBend_time_h.Text & ":" & CBend_time_m.Text
    'End Sub

    Sub AddSlot(ByVal capacity As Int32, ByVal SlotDate As Date)
        Dim sql As String = ""
        sql = "delete from TB_APPOINTMENT_SLOT where CONVERT(varchar(8),app_date,112)  = '" & FixDate(SlotDate) & "'"
        executeSQL(sql)
        For x As Int32 = 0 To flpTime.Controls.Count - 1
            If flpTime.Controls(x).BackColor = Color.ForestGreen Then
                Dim ID As Int32 = FindID("TB_APPOINTMENT_SLOT")
                sql = "insert into TB_APPOINTMENT_SLOT(id,app_date,capacity,slot_time,in_use,create_by,create_date) values(" & ID & ",'" & FixDate(SlotDate) & "'," & capacity & ",'" & FixDate(SlotDate) & " " & flpTime.Controls(x).Text & "',0," & myUser.user_id & ",getdate())"
                executeSQL(sql)
            End If
        Next
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

    Private Sub frmAppointment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim dt As New DataTable
        Dim sql As String = "select config_name, config_value from tb_setting where config_name in ('s_open','s_close') order by config_name"
        dt = getDataTable(sql)
        If dt.Rows.Count = 2 Then
            'CBstart_time_h.Text & ":" & CBstart_time_m.Text & "   To   " & CBend_time_h.Text & ":" & CBend_time_m.Text
            lblShopOpen.Text = dt.Rows(1)("config_value").ToString & " To " & dt.Rows(0)("config_value").ToString
        End If
        dt.Dispose()

    End Sub
End Class