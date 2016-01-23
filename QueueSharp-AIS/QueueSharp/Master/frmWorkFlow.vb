Public Class frmWorkFlow

    Dim dt_item_user As New DataTable
    Dim dt_slot As New DataTable
    Dim dt_Item As New DataTable
    Dim dt_App As New DataTable
    Dim dt_user As New DataTable
    Dim LastDate As Int32 = 0

    Private Sub AddButton()
        Dim sql As String = ""

        sql = "select id,item_code,item_name from tb_item where active_status = 1 order by item_order"
        dt_Item = getDataTable(sql)
        GridItem.DataSource = dt_Item

        sql = "select user_id "
        sql += " from TB_USER_SKILL "
        sql += " left join TB_SKILL on TB_USER_SKILL.skill_id = TB_SKILL.id "
        sql += " where TB_SKILL.active_status = 1 and TB_SKILL.appointment = 1"
        Dim dt_user_app As New DataTable
        dt_user_app = getDataTable(sql)

        sql = "select x.id,title_name + ' ' + fname + ' ' + lname as fullname "
        sql += " from TB_USER x "
        sql += " left join TB_TITLE y on x.title_id = y.id "
        sql += " where x.active_status = 1 "
        sql += " order by fname"
        dt_user = getDataTable(sql)
        If dt_user.Rows.Count > 0 Then
            FLP.Width = 375 + (dt_Item.Rows.Count * 65) + 35
            FLP.Height = 20 * dt_user.Rows.Count
            For i As Int32 = 0 To dt_user.Rows.Count - 1
                AddUser(dt_user.Rows(i).Item("id"), dt_user.Rows(i).Item("fullname"), 20 * (i + 1))

                Dim ret As Boolean = False
                dt_user_app.DefaultView.RowFilter = "user_id = " & dt_user.Rows(i).Item("id")
                If dt_user_app.DefaultView.Count > 0 Then
                    ret = True
                End If

                AddAppointment(dt_user.Rows(i).Item("id"), ret)
                For j As Int32 = 0 To dt_Item.Rows.Count - 1
                    Dim Status As Boolean = False
                    dt_item_user.DefaultView.RowFilter = "user_id = " & dt_user.Rows(i).Item("id") & " and item_id = " & dt_Item.Rows(j).Item("id")
                    If dt_item_user.DefaultView.Count > 0 Then
                        Status = True
                    End If
                    AddService(dt_Item.Rows(j).Item("item_code"), dt_user.Rows(i).Item("id"), dt_Item.Rows(j).Item("id"), Status)
                    dt_item_user.DefaultView.RowFilter = ""
                Next

                'Dim ret As Boolean = False
                'dt_user_app.DefaultView.RowFilter = "user_id = " & dt.Rows(i).Item("id")
                'If dt_user_app.DefaultView.Count > 0 Then
                '    ret = True
                'End If
                'AddAppointment(dt.Rows(i).Item("id"), ret)
            Next
        End If
    End Sub

    Sub AddUser(ByVal RowID As String, ByVal Name As String, ByVal y As Integer)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        With lbl
            .Location = New Point(0, y)
            .AutoSize = False
            .Width = 250
            .Height = 20
            .Name = "US_" & RowID
            .Text = Name
            .Tag = "User"
            .BackColor = Color.White
            .ForeColor = Color.Black
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.FixedSingle
            .Font = Font
        End With
        FLP.Controls.Add(lbl)
    End Sub

    Sub AddService(ByVal Item As String, ByVal UserId As Int32, ByVal ItemId As Int32, ByVal Status As Boolean)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        With lbl
            .AutoSize = False
            .Width = 60
            .Height = 20
            .Tag = UserId.ToString & "," & ItemId.ToString
            .Name = "lbl" & UserId.ToString & "_" & ItemId.ToString
            If Status = True Then
                .Text = Item
                .BackColor = Color.DarkOrange
                .ForeColor = Color.White
            Else
                .BackColor = Color.LightGray
                .Text = Item
                .Enabled = False
            End If
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
            .Font = Font
        End With
        FLP.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf CheckStatus
    End Sub

    Sub AddAppointment(ByVal UserId As Int32, ByVal Status As Boolean)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        With lbl
            .AutoSize = False
            .Width = 125
            .Height = 20
            '.Image = Global.QueueSharp.My.Resources.Resources.calendar
            .Font = Font
            .Name = "App_" & UserId
            .BorderStyle = BorderStyle.FixedSingle
            .TextAlign = ContentAlignment.MiddleCenter
            If Status = True Then
                .Enabled = True
                .BackColor = Color.WhiteSmoke
                .Text = "Assign"
                .ForeColor = Color.Black
            Else
                .Enabled = False
                .BackColor = Color.LightGray
            End If
        End With
        FLP.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf Appointment
    End Sub

    Private Sub Appointment(ByVal Sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = Sender
        If lbl.BackColor = Color.WhiteSmoke Then
            lbl.BackColor = Color.RoyalBlue
            lbl.ForeColor = Color.White
        Else
            lbl.BackColor = Color.WhiteSmoke
            lbl.ForeColor = Color.Black
        End If
        ''If MC.SelectDate = False Then
        ''    MessageBox.Show("Please select Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ''    Exit Sub
        ''End If

        'If Calendar1.DateValue = "" Then
        '    MessageBox.Show("Please select Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        'Dim lbl As Label = Sender
        'Dim f As New frmAppointment
        'f.UserID = lbl.Name
        ''f.SelectDate = MC.Year & MC.Month.ToString.PadLeft(2, "0") & MC.Day.ToString.PadLeft(2, "0")
        'f.SelectDate = Calendar1.StartDate.Year & Calendar1.StartDate.Month.ToString.PadLeft(2, "0") & Calendar1.StartDate.Day.ToString.PadLeft(2, "0")
        'f.SelectDateTo = Calendar1.EndDate.Year & Calendar1.EndDate.Month.ToString.PadLeft(2, "0") & Calendar1.EndDate.Day.ToString.PadLeft(2, "0")
        'If f.ShowDialog() = Windows.Forms.DialogResult.Yes Then
        '    Dim dt As New DataTable
        '    'dt_App.DefaultView.RowFilter = " YY = " & MC.Year & " and MM = " & MC.Month & " and DD = " & MC.Day
        '    dt_App.DefaultView.RowFilter = " YY = " & Calendar1.Year & " and MM = " & Calendar1.Month & " and DD = " & Calendar1.Day
        '    dt = dt_App.DefaultView.ToTable
        '    If dt.Rows.Count > 0 Then
        '        For i As Int32 = 0 To dt.Rows.Count - 1
        '            FLP.Controls.Item(dt.Rows(i).Item("user_id").ToString).BackColor = Color.LightBlue
        '        Next
        '    End If
        'End If

    End Sub

    Private Sub CheckStatus(ByVal Sender As Object, ByVal e As EventArgs)
        'If MC.SelectDate = False Then
        '    MessageBox.Show("Please select Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        'If Calendar1.DateValue = "" Then
        '    MessageBox.Show("Please select Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If
        'If Calendar1.StartDate < Today.Date Then
        '    MessageBox.Show("Invalid Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        Dim btn As Label = Sender
        'Dim DaySelect As Int32 = 0
        'Dim StartDate As Date = FindDateNow
        'Dim sql As String = ""
        'Dim UserID As Int32
        'Dim ItemID As Int32
        'Dim tmp As String()

        'tmp = Split(btn.Tag, ",")
        'UserID = tmp(0)
        'ItemID = tmp(1)

        'Dim stDate As Date = Calendar1.StartDate
        'Do
        '    'Dim DD As String = CStr(MC.Year) & CStr(MC.Month).PadLeft(2, "0") & CStr(MC.Day).PadLeft(2, "0")
        '    Dim DD As String = CStr(stDate.Year) & CStr(stDate.Month).PadLeft(2, "0") & CStr(stDate.Day).PadLeft(2, "0")

        '    Dim Priority As Int32 = 0
        '    Dim dt As New DataTable
        '    sql = "select priority from TB_USER_SERVICE_SCHEDULE where user_id = " & UserID & " and active_status = 1 and priority = 1 and datediff(d,service_date,'" & DD & "')=0"
        '    dt = getDataTable(sql)
        '    If dt.Rows.Count = 0 Then
        '        'ไม่มี Primary Priority 
        '        Priority = 1
        '    End If


        If btn.BackColor = Color.Firebrick Then
            'If Priority = 1 Then
            '    btn.BackColor = Color.ForestGreen
            'Else
            '    btn.BackColor = Color.DarkOrange
            'End If
            'ExcuteData(DD, UserID, ItemID, 1, Priority)
            btn.BackColor = Color.DarkOrange
        ElseIf btn.BackColor = Color.ForestGreen Then
            'Sql = "select priority from TB_USER_SERVICE_SCHEDULE where user_id = " & UserID & " and active_status = 1 and priority = 0 and datediff(d,service_date,'" & DD & "')=0"
            'dt = getDataTable(Sql)
            'If dt.Rows.Count > 0 Then
            '    MessageBox.Show("Please cancel Skill Active Sceondry !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'Else
            '    btn.BackColor = Color.Firebrick
            '    ExcuteData(DD, UserID, ItemID, 0, 0)
            'End If
            btn.BackColor = Color.Firebrick
        Else
            'btn.BackColor = Color.Firebrick
            'ExcuteData(DD, UserID, ItemID, 0, 0)
            btn.BackColor = Color.ForestGreen
        End If
        'stDate = DateAdd(DateInterval.Day, 1, stDate)
        'Loop While stDate <= Calendar1.EndDate
        'SetUserService()
        'SetDateEvent()
    End Sub

    Sub ExcuteData(ByVal StartDate As String, ByVal UserID As Int32, ByVal ItemID As Int32, ByVal Status As Int32, ByVal Priority As Int32)
        Dim sql As String = ""
        Dim dt As New DataTable

        sql = "select COUNT(1) as num from TB_USER_SERVICE_SCHEDULE where datediff(d,'" & StartDate & "',service_date)= 0 and user_id = " & UserID & " and item_id = " & ItemID
        dt = getDataTable(sql)
        If dt.Rows(0).Item("num") = 0 Then
            sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_USER_SERVICE_SCHEDULE); insert into TB_USER_SERVICE_SCHEDULE(id,service_date,user_id,item_id,active_status,update_by,update_date,priority) values(@ID,'" & StartDate & "'," & UserID & "," & ItemID & "," & Status & "," & myUser.user_id & ",getdate()," & Priority & ")"
        Else
            sql = "update TB_USER_SERVICE_SCHEDULE set service_date = '" & StartDate & "',user_id = " & UserID & ",item_id = " & ItemID & ",active_status = " & Status & ",update_by = " & myUser.user_id & ",update_date = getdate(),priority = " & Priority & " where datediff(d,'" & StartDate & "',service_date)= 0 and user_id = " & UserID & " and item_id = " & ItemID
        End If
        executeSQL(sql)
    End Sub

    Sub UpdateDataSlot()
        Dim sql As String = ""
        sql = "select id,convert(varchar(8),service_date,112) as service_date,user_id,item_id,active_status,year(service_date) as YY,month(service_date) as MM,day(service_date) as DD,isnull(priority,0) as priority from TB_USER_SERVICE_SCHEDULE where active_status = 1 and item_id not in (select id from TB_ITEM where active_status = 0) and user_id not in (select id from TB_USER where active_status = 0)"
        dt_slot = getDataTable(sql)

        'sql = "select distinct convert(varchar(8),app_date,112) as app_date,user_id,year(app_date) as YY,month(app_date) as MM,day(app_date) as DD from TB_APPOINTMENT_SCHEDULE where datediff(d,getdate(),app_date) >= 0"
        'dt_App = getDataTable(sql)

    End Sub

    Private Sub frmWorkFlow_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim sql As String = ""
        sql = "select user_id,b.item_id "
        sql += " from TB_USER_SKILL a "
        sql += " left join TB_SKILL_ITEM b on a.skill_id = b.skill_id "
        sql += " left join TB_SKILL c on a.skill_id = c.id "
        sql += " left join TB_ITEM d on b.item_id = d.id "
        sql += " where c.active_status = 1 and d.active_status = 1 group by user_id,b.item_id"
        dt_item_user = getDataTable(sql)

        UpdateDataSlot()
        Me.SuspendLayout()
        Me.FLP.SuspendLayout()
        AddButton()
        FLP.ResumeLayout(True)
        Me.ResumeLayout(True)
        sql = "select id,item_code,item_name from TB_ITEM where active_status = 1"
        Dim dt As New DataTable
        dt = getDataTable(sql)

        Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        GridItem.Font = Font

        SetUserService()
        SetDateEvent()
    End Sub

    Private Sub frmWorkFlow_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Public Function FormatDate(ByVal StringDate As String) As String 'Convert วันที่ให้เป็น YYYYMMDD
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
            Return d.ToString.PadLeft(2, "0") & "/" & m.ToString.PadLeft(2, "0") & "/" & y.ToString
        Else
            Return ""
        End If
    End Function


    'Private Sub MC_onValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MC.onValueChanged
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "declare @Month as smallint;select @Month = " & MC.Month & ";declare @Year as smallint;select @Year = " & MC.Year & ";select DAY(service_date) as DD from dbo.TB_USER_SERVICE_SCHEDULE where active_status = 1 and MONTH(service_date) = @Month and YEAR(service_date) = @Year and datediff(d,getdate(),service_date) >= 0 group by DAY(service_date) order by DAY(service_date)"
    '    dt = getDataTable(sql)
    '    MC.dt_SeleteDate = dt

    'End Sub

    Private Sub MC_onDateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MC.onDateChanged
        SetUserService()
    End Sub

    Private Sub SetUserService()
        UpdateDataSlot()
        Dim sql As String = ""
        Dim dt As New DataTable
        '************************ เคลีย์ค่าสี ***************************
        For x As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(x).Enabled = True And FLP.Controls(x).BackColor <> Color.Firebrick And FLP.Controls(x).Text <> "Assign" Then
                If FLP.Controls(x).ForeColor <> Color.Black And FLP.Controls(x).BackColor <> Color.White Then
                    If FLP.Controls(x).Enabled = True Then
                        FLP.Controls(x).BackColor = Color.Firebrick
                    End If
                End If
            End If
        Next
        ''**************************************************************
        ''************************** ไล่สี ***************************
        For i As Int32 = 0 To dt_user.Rows.Count - 1
            If FLP.Controls.Item("App_" & dt_user.Rows(i).Item("id").ToString).Enabled = True Then
                FLP.Controls.Item("App_" & dt_user.Rows(i).Item("id").ToString).BackColor = Color.WhiteSmoke
                FLP.Controls.Item("App_" & dt_user.Rows(i).Item("id").ToString).ForeColor = Color.Black
            End If
        Next
        dt = New DataTable
        sql = "select * from TB_APPOINTMENT_USER where CONVERT(varchar(8),app_date,112)= '" & FixDate(Calendar1.StartDate) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For x As Int32 = 0 To dt.Rows.Count - 1
                Try
                    If FLP.Controls.Item("App_" & dt.Rows(x).Item("user_id").ToString).Enabled = True Then
                        FLP.Controls.Item("App_" & dt.Rows(x).Item("user_id").ToString).BackColor = Color.RoyalBlue
                        FLP.Controls.Item("App_" & dt.Rows(x).Item("user_id").ToString).ForeColor = Color.WhiteSmoke
                    End If

                Catch ex As Exception : End Try
            Next

        End If

        dt = New DataTable
        dt_slot.DefaultView.RowFilter = " YY = " & Calendar1.Year & " and MM = " & Calendar1.Month & " and DD = " & Calendar1.Day
        dt = dt_slot.DefaultView.ToTable
        If dt.Rows.Count > 0 Then
            'ถ้ามีข้อมูล
            For i As Int32 = 0 To dt.Rows.Count - 1
                Try
                    If dt.Rows(i).Item("priority").ToString = "1" Then
                        If FLP.Controls.Item("lbl" & dt.Rows(i).Item("user_id").ToString & "_" & dt.Rows(i).Item("item_id").ToString).Enabled = True Then
                            FLP.Controls.Item("lbl" & dt.Rows(i).Item("user_id").ToString & "_" & dt.Rows(i).Item("item_id").ToString).BackColor = Color.ForestGreen
                        End If
                    Else
                        If FLP.Controls.Item("lbl" & dt.Rows(i).Item("user_id").ToString & "_" & dt.Rows(i).Item("item_id").ToString).Enabled = True Then
                            FLP.Controls.Item("lbl" & dt.Rows(i).Item("user_id").ToString & "_" & dt.Rows(i).Item("item_id").ToString).BackColor = Color.DarkOrange
                        End If
                    End If
                Catch ex As Exception : End Try
            Next
        Else
            'ถ้าไม่มีข้อมูล
            If Calendar1.StartDate.Date >= FindDateNow.Date Then
                For i As Int32 = 0 To FLP.Controls.Count - 1
                    If FLP.Controls(i).Enabled = True Then
                        Select Case FLP.Controls(i).BackColor
                            'Firebrick
                            Case Color.Firebrick
                                FLP.Controls(i).BackColor = Color.DarkOrange
                                FLP.Controls(i).ForeColor = Color.White
                            Case Color.ForestGreen
                                FLP.Controls(i).BackColor = Color.DarkOrange
                                FLP.Controls(i).ForeColor = Color.White
                            Case Color.SteelBlue
                                FLP.Controls(i).BackColor = Color.WhiteSmoke
                                FLP.Controls(i).ForeColor = Color.Black
                        End Select
                    End If
                Next
            End If

        End If
        ''**************************************************************

    End Sub

    Private Sub SetDateEvent()

        Calendar1.ClearEventDate()
        Dim DayInMonth As Date = Calendar1.StartDate
        Dim sql As String = "select distinct service_date "
        sql += " from TB_USER_SERVICE_SCHEDULE "
        sql += " where convert(varchar(8),service_date,112) between '" & DayInMonth.Year & DayInMonth.Month.ToString.PadLeft(2, "0") & "01' and '" & DayInMonth.Year & DayInMonth.Month.ToString.PadLeft(2, "0") & DayInMonth.DaysInMonth(DayInMonth.Year, DayInMonth.Month) & "' "
        sql += " and active_status = '1' "
        Dim dt As DataTable = getDataTable(sql)
        Calendar1.SetEventDates(dt, "service_date")

    End Sub

    Private Sub Calendar1_DateChange(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles Calendar1.DateChange
        If Calendar1.StartDate = Calendar1.EndDate Then
            If FLP.Controls.Count > 0 Then
                SetUserService()
                SetDateEvent()
            End If
        ElseIf Calendar1.StartDate > FindDateNow() Then
            For i As Int32 = 0 To FLP.Controls.Count - 1
                If FLP.Controls(i).Enabled = True Then
                    Select Case FLP.Controls(i).BackColor
                        Case Color.Firebrick
                            FLP.Controls(i).BackColor = Color.DarkOrange
                            FLP.Controls(i).ForeColor = Color.White
                        Case Color.ForestGreen
                            FLP.Controls(i).BackColor = Color.DarkOrange
                            FLP.Controls(i).ForeColor = Color.White
                        Case Color.SteelBlue
                            FLP.Controls(i).BackColor = Color.WhiteSmoke
                            FLP.Controls(i).ForeColor = Color.Black
                    End Select
                End If
            Next
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Calendar1.DateValue = "" Then
            MessageBox.Show("Please select Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If Calendar1.StartDate < Today.Date Then
            MessageBox.Show("Invalid Date !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        For i As Int32 = 0 To dt_user.Rows.Count - 1
            Dim green As Int32 = 0
            Dim orange As Int32 = 0
            Dim red As Int32 = 0
            For x As Int32 = 0 To dt_Item.Rows.Count - 1
                Select Case FLP.Controls("lbl" & dt_user.Rows(i).Item("id").ToString & "_" & dt_Item.Rows(x).Item("id").ToString).BackColor
                    Case Color.ForestGreen
                        green = green + 1
                    Case Color.Firebrick
                        red = red + 1
                    Case Color.DarkOrange
                        orange = orange + 1
                End Select
            Next
            'If orange > 0 Then
            '    MessageBox.Show("Please select primary service" & vbCrLf & "This user " & dt_user.Rows(i).Item("fullname").ToString & " !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If
            If green > 1 Then
                MessageBox.Show("Please select one primary service" & vbCrLf & "This user " & dt_user.Rows(i).Item("fullname").ToString & " !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf green = 0 Then
                MessageBox.Show("Please select primary service" & vbCrLf & "This user " & dt_user.Rows(i).Item("fullname").ToString & " !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Next
        'select * from TB_USER_SERVICE_SCHEDULE
        Dim sql As String = ""
        'Dim StartDate As String = ""
        'Dim EndDate As String = ""
        'Dim ST As Int32 = 0
        'Dim ET As Int32 = 0
        'StartDate = Calendar1.StartDate.Year & Calendar1.StartDate.Month.ToString.PadLeft(2, "0") & Calendar1.StartDate.Day.ToString.PadLeft(2, "0")
        'EndDate = Calendar1.EndDate.Year & Calendar1.EndDate.Month.ToString.PadLeft(2, "0") & Calendar1.EndDate.Day.ToString.PadLeft(2, "0")
        

        'ST = CInt(Calendar1.StartDate.Year & Calendar1.StartDate.Month.ToString.PadLeft(2, "0"))
        'ET = CInt(Calendar1.EndDate.Year & Calendar1.EndDate.Month.ToString.PadLeft(2, "0"))
        'Dim DateCount As Int32 = 0
        ''เลือกในเดือนเดียวกัน
        'If ST = ET Then
        '    DateCount = (CInt(Calendar1.EndDate.Day) - CInt(Calendar1.StartDate.Day)) + 1
        'Else
        '    '******** ค่อยแก้เพิ่มเติม ทำให้เลือกได้เดือนเดียวก่อน *********
        '    MessageBox.Show("Please select one month !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        '    '************************************************
        '    DateCount = (ET - ST) + 1
        'End If

        sql = "delete from TB_USER_SERVICE_SCHEDULE where convert(varchar(8),service_date,112) between '" & FixDate(Calendar1.StartDate) & "' and '" & FixDate(Calendar1.EndDate) & "'"
        executeSQL(sql)
        sql = "delete from TB_APPOINTMENT_USER where convert(varchar(8),app_date,112) between '" & FixDate(Calendar1.StartDate) & "' and '" & FixDate(Calendar1.EndDate) & "'"
        executeSQL(sql)
        Dim Selectdate As Date = Calendar1.StartDate.Date

        For i As Int32 = 0 To 1000
            If Selectdate <= Calendar1.EndDate.Date Then
                For x As Int32 = 0 To dt_user.Rows.Count - 1
                    For y As Int32 = 0 To dt_Item.Rows.Count - 1
                        Dim Primary As Int32 = 3

                        Select Case FLP.Controls("lbl" & dt_user.Rows(x).Item("id").ToString & "_" & dt_Item.Rows(y).Item("id").ToString).BackColor
                            Case Color.ForestGreen
                                Primary = 1
                            Case Color.DarkOrange
                                Primary = 0
                        End Select

                        If Primary <> 3 Then
                            sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_USER_SERVICE_SCHEDULE); insert into TB_USER_SERVICE_SCHEDULE(id,service_date,user_id,item_id,active_status,update_by,update_date,priority) values(@ID,'" & FixDate(Selectdate) & "'," & dt_user.Rows(x).Item("id").ToString & "," & dt_Item.Rows(y).Item("id").ToString & ",1," & myUser.user_id & ",getdate()," & Primary & ")"
                            executeSQL(sql)
                        End If
                    Next

                    '****************
                    If FLP.Controls("App_" & dt_user.Rows(x).Item("id").ToString).BackColor = Color.RoyalBlue Then
                        sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_APPOINTMENT_USER); insert into TB_APPOINTMENT_USER(id,app_date,user_id,update_by,update_date) values(@ID,'" & FixDate(Selectdate) & "'," & dt_user.Rows(x).Item("id").ToString & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If

                Next
                Selectdate = Selectdate.AddDays(1)
            Else
                Exit For
            End If
        Next
        
        'StartDate = Calendar1.StartDate.Day.ToString.PadLeft(2, "0") & "/" & Calendar1.StartDate.Month.ToString.PadLeft(2, "0") & "/" & Calendar1.StartDate.Year + 543
        'Dim ST_Date As Date = CDate(StartDate)
        'For i As Int32 = 0 To DateCount - 1
        '    Dim DateInsert As String = DateAdd(DateInterval.Day, i, ST_Date)
        '    For x As Int32 = 0 To dt_user.Rows.Count - 1
        '        For y As Int32 = 0 To dt_Item.Rows.Count - 1
        '            Select Case FLP.Controls("lbl" & dt_user.Rows(x).Item("id").ToString & "_" & dt_Item.Rows(y).Item("id").ToString).BackColor
        '                Case Color.ForestGreen
        '                    sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_USER_SERVICE_SCHEDULE); insert into TB_USER_SERVICE_SCHEDULE(id,service_date,user_id,item_id,active_status,update_by,update_date,priority) values(@ID,'" & FixDate(DateInsert) & "'," & dt_user.Rows(x).Item("id").ToString & "," & dt_Item.Rows(y).Item("id").ToString & ",1," & myUser.user_id & ",getdate(),1)"
        '                Case Color.DarkOrange
        '                    sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_USER_SERVICE_SCHEDULE); insert into TB_USER_SERVICE_SCHEDULE(id,service_date,user_id,item_id,active_status,update_by,update_date,priority) values(@ID,'" & FixDate(DateInsert) & "'," & dt_user.Rows(x).Item("id").ToString & "," & dt_Item.Rows(y).Item("id").ToString & ",1," & myUser.user_id & ",getdate(),0)"
        '            End Select
        '            executeSQL(sql)
        '        Next
        '    Next
        'Next
        SetDateEvent()
        MessageBox.Show("Your input data is successfully been saved.", "Complete", MessageBoxButtons.OK)
    End Sub

    Private Sub Calendar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar1.Load

    End Sub
End Class