Imports System.Data.SqlClient

Public Class frmMoveQueue

    'Dim ds As New DataSet
    'Dim ListOld As New DataTable
    'Dim ListNew As New DataTable

    'Private Sub frmViewDoctorService_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    '    Me.WindowState = FormWindowState.Minimized
    '    Me.WindowState = FormWindowState.Maximized
    '    Me.ControlBox = False
    '    Me.CheckTimer.Checked = True
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "select id,item_name from TB_ITEM where q_type_id = 1 and active_status = 1  order by id"
    '    dt = getDataTable(sql)
    '    If dt.Rows.Count > 0 Then
    '        cbItem.DataSource = dt
    '        cbItem.DisplayMember = "item_name"
    '        cbItem.ValueMember = "id"
    '        cbItem.SelectedIndex = 0
    '        showdata(cbItem.SelectedValue)
    '    End If
    'End Sub

    'Sub showdata(ByVal ItemID As Int32) ' Run FirstTime Only

    '    tlp.Controls.Clear()
    '    Me.SuspendLayout()
    '    Dim sql As String = ""
    '    Dim dt As New DataTable

    '    sql = "select count(1) as room from TB_ITEM_COUNTER where item_id = " & ItemID & " and counter_id not in (select id from TB_COUNTER where active_status = 0)"
    '    dt = getDataTable(sql)
    '    'หาจำนวนห้องหมอ
    '    Dim room As Int32 = CInt(dt.Rows(0).Item("room"))

    '    sql = "select TB_COUNTER.id,TB_COUNTER.counter_name,TB_COUNTER.hold,case when DATEDIFF(d,TB_COUNTER.login_date,GETDATE()) = 0 then TB_TITLE.title_name + ' ' + TB_USER.fname + ' ' + TB_USER.lname else '' end  as name from TB_ITEM_COUNTER left join TB_COUNTER on TB_ITEM_COUNTER.counter_id = TB_COUNTER.id left join TB_USER on TB_COUNTER.login_by = TB_USER.id left join TB_TITLE on TB_USER.title_id = TB_TITLE.id where TB_COUNTER.active_status = 1 and TB_ITEM_COUNTER.item_id = " & ItemID
    '    dt = getDataTable(sql)
    '    ListOld = dt

    '    Dim k As Int32 = 1
    '    Dim j As Int32 = 0
    '    For i As Int32 = 1 To room
    '        Dim dt_tmp As New DataTable
    '        Dim lbl As New CodeVendor.Controls.Grouper
    '        ''*********** Service ภายในห้อง ************
    '        'Dim itemdoctor As String = ""
    '        'sql = "select distinct item_id from TB_service_counter_item where counter_id = '" & FixNULL(dt.Rows(i)("counter_id").ToString) & "'"
    '        'ds = getDataset(sql)
    '        'If ds.Tables(0).Rows.Count > 0 Then
    '        '    For l As Int32 = 0 To ds.Tables(0).Rows.Count - 1
    '        '        If itemdoctor = "" Then
    '        '            itemdoctor = FixNULL(ds.Tables(0).Rows(l)("item_id").ToString)
    '        '        Else
    '        '            itemdoctor = itemdoctor & "','" & FixNULL(ds.Tables(0).Rows(l)("item_id").ToString)
    '        '        End If
    '        '    Next
    '        'End If
    '        ''****************************************

    '        lbl.GroupTitle = dt.Rows(i)("counter_name").ToString & " Customer Served : " & dt.Rows(i)("name").ToString
    '        Dim Grid As New DataGridView
    '        'Dim cs As New DataGridViewCellStyle
    '        'cs.Font = New Font("Ms Sans Serif", 8, FontStyle.Bold)
    '        'Grid.ColumnHeadersDefaultCellStyle = cs
    '        Grid.AllowUserToAddRows = False
    '        Grid.AllowUserToDeleteRows = False
    '        Grid.AllowUserToOrderColumns = True
    '        Grid.AllowUserToResizeColumns = True
    '        Grid.AllowUserToResizeRows = False
    '        Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
    '        Grid.RowHeadersVisible = False
    '        Grid.MultiSelect = False
    '        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    '        Grid.EditMode = DataGridViewEditMode.EditProgrammatically
    '        Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    '        'sql = "select case when isnull(e.queue_no,'') = '' then 'No' else 'Yes' end as Appoint ,a.queue_no as QueueNo,isnull(b.prename_desc,'') + ' ' + isnull(c.f_name,'') + ' ' + isnull(c.l_name,'') as Name,d.item_name as Service,datediff(n,a.service_time,getdate())as Time,a.counter_id ,a.item_id,case when a.status = 1 then 'Waiting' else 'Hold' end as Status,a.status as sta from TB_counter_queue a inner join TB_customer c on a.customer_id = c.customer_id left outer join TB_prename b on c.prename_id = b.prename_id inner join TB_service_item d on a.item_id = d.item_id left join TB_customer_doctor e on datediff(d,a.service_date,s_date)=0 and a.customer_id = e.customer_id where a.counter_id ='" & dt.Rows(i)("counter_id").ToString & "' and a.status in (1,6) and datediff(d,a.service_date,getdate())=0 order by a.service_time"
    '        sql = "select queue_no,customer_id,datediff(n,start_time,getdate())as Time,case when status = 1 then 'Waiting' else 'Hold' end as Status from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and status in (1,6) and counter_id = " & dt.Rows(i)("counter_id")
    '        dt_tmp = getDataTable(sql)
    '        Grid.DataSource = dt_tmp
    '        Grid.Dock = DockStyle.Fill
    '        'Grid.Columns.Add()

    '        Grid.Tag = dt.Rows(i)("counter_id").ToString
    '        Grid.BorderStyle = BorderStyle.None
    '        lbl.BackgroundColor = Color.White
    '        lbl.BackgroundGradientColor = Color.Orange
    '        If dt.Rows(i)("hold").ToString = "N" Then
    '            lbl.BackgroundGradientColor = Color.Silver
    '        End If

    '        lbl.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
    '        lbl.Controls.Add(Grid)
    '        Grid.Location = New System.Drawing.Point(12, 25)
    '        tlp.Controls.Add(lbl)
    '        Grid.AllowDrop = True
    '        AddHandler Grid.DragDrop, AddressOf ListViewDragDrop
    '        AddHandler Grid.DragEnter, AddressOf ListViewDragEnter
    '        AddHandler Grid.CellMouseDown, AddressOf DataGridView1_CellMouseDown
    '        AddHandler Grid.DragLeave, AddressOf DataGridView1_DragLeave
    '    Next
    '    Me.ResumeLayout(False)

    'End Sub

    'Public Sub New()
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub

    'Private Sub TimerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRefresh.Tick
    '    TimerRefresh.Enabled = False
    '    RefreshAllRoom(cbItem.SelectedValue)
    '    If Me.CheckTimer.Checked Then
    '        TimerRefresh.Enabled = True
    '    End If
    'End Sub

    ''New Sub Routine
    'Private Sub RefreshAllRoom(ByVal ItemID As Int32)
    '    Me.TimerRefresh.Enabled = False
    '    Me.SuspendLayout()
    '    Dim dt As New DataTable
    '    Dim i As Integer
    '    Dim sql As String = ""
    '    'หาจำนวนห้องหมอที่แตกต่างกัน

    '    sql = "select count(1) as room from TB_ITEM_COUNTER where item_id = " & ItemID & " and counter_id not in (select id from TB_COUNTER where active_status = 0)"
    '    dt = getDataTable(sql)

    '    Dim room As Int32 = dt.Rows(0)("room")
    '    If room <> ListOld.Rows.Count Then 'Check If Number Doctor Room is Not Match
    '        dt.Dispose()
    '        showdata(ItemID)
    '        Exit Sub
    '    End If
    '    'Check If Number Doctor Room is  Match but every some Room Is Not Match
    '    sql = "select TB_COUNTER.id,TB_COUNTER.counter_name,TB_COUNTER.hold,case when DATEDIFF(d,TB_COUNTER.login_date,GETDATE()) = 0 then TB_TITLE.title_name + ' ' + TB_USER.fname + ' ' + TB_USER.lname else '' end  as name from TB_ITEM_COUNTER left join TB_COUNTER on TB_ITEM_COUNTER.counter_id = TB_COUNTER.id left join TB_USER on TB_COUNTER.login_by = TB_USER.id left join TB_TITLE on TB_USER.title_id = TB_TITLE.id where TB_COUNTER.active_status = 1 and TB_ITEM_COUNTER.item_id = " & ItemID
    '    dt = getDataTable(sql)
    '    ListNew = dt

    '    For i = 1 To room
    '        If ListNew.Rows(i).Item("counter_id") <> ListOld.Rows(i).Item("counter_id") Then
    '            dt.Dispose()
    '            showdata(ItemID)
    '            Exit Sub
    '        End If
    '    Next
    '    'Run Update Every Room
    '    For i = 1 To room
    '        Dim Grid As DataGridView = tlp.Controls(i).Controls(0)
    '        sql = "select queue_no,customer_id,counter_id,datediff(n,start_time,getdate())as Time,case when status = 1 then 'Waiting' else 'Hold' end as Status from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and status in (1,6) and counter_id" & dt.Rows(i)("counter_id")
    '        dt = getDataTable(sql)
    '        Grid.DataSource = dt
    '    Next
    '    Me.ResumeLayout(False)
    '    Me.TimerRefresh.Enabled = True
    'End Sub
    ''End New

    'Function checkAutoRefresh() As Boolean
    '    If CheckTimer.Checked Then
    '        timerRefresh.Interval = CInt(DomainUp.Value) * 1000
    '        timerRefresh.Enabled = True
    '        DomainUp.Enabled = True
    '        ButRefresh.Enabled = False
    '    Else
    '        timerRefresh.Enabled = False
    '        DomainUp.Enabled = False
    '        ButRefresh.Enabled = True
    '    End If
    'End Function

    'Private Sub CheckTimer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckTimer.CheckedChanged
    '    checkAutoRefresh()
    'End Sub

    ''*****************************************************
    'Private Sub ListViewItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) 'Handles ListView1.ItemDrag, ListView2.ItemDrag
    '    DoDragDrop(e.Item, DragDropEffects.Move)
    'End Sub

    'Private Sub ListViewDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) 'Handles ListView1.DragEnter, ListView2.DragEnter
    '    e.Effect = DragDropEffects.Move
    '    CType(sender, DataGridView).BorderStyle = BorderStyle.FixedSingle
    'End Sub

    'Private Sub ListViewDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) 'Handles ListView1.DragDrop, ListView2.DragDrop

    '    'Dim lvItem As ListViewItem
    '    'Dim destItem As ListViewItem
    '    'Dim destLv As DataGridViewRow = CType(sender, DataGridViewRow)
    '    '***************************************
    '    'e.Data.GetDataPresent("String", False)
    '    'MsgBox(CType(e.Data.GetData("System.Windows.Forms.DataGridViewCell", False), System.Windows.Forms.DataGridViewCell).Value.ToString)]
    '    If e.Data.GetDataPresent("System.Windows.Forms.DataGridViewRow", False) Then
    '        Dim obj As New DataGridViewRow
    '        obj = e.Data.GetData("System.Windows.Forms.DataGridViewRow", False)
    '        If obj.Cells(1).Value.ToString = "" Or obj.Cells(5).Value.ToString = "" Then Exit Sub

    '        Dim Grid As DataGridView = sender
    '        Dim CounterID As String = obj.Cells(5).Value
    '        Dim TB_No As String = obj.Cells(1).Value
    '        If Grid.Tag <> CounterID Then
    '            'เช็คสถานะว่าห้องที่จะไปถูก Hold ไว้หรือไม่
    '            ds = getDataset("select status,counter_name from TB_service_counter where counter_id ='" & Grid.Tag & "'")
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                If ds.Tables(0).Rows(0)("status").ToString = "2" Then
    '                    If (MessageBox.Show("The room " & ds.Tables(0).Rows(0)("counter_name").ToString & " is on-hold." & vbCrLf & "Do you really want to move?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.No Then
    '                        Exit Sub
    '                    End If
    '                ElseIf obj.Cells(6).Value.ToString = "899688D9-48F6-4C4C-BD7C-1475F4FB00F6" Then
    '                    If (MessageBox.Show("The customer is waiting for examination results." & vbCrLf & "Do you really want to move?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.No Then
    '                        Exit Sub
    '                    End If
    '                ElseIf obj.Cells(8).Value.ToString = "6" Then
    '                    If (MessageBox.Show("The customer is HOLD." & vbCrLf & "Do you really want to move?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.No Then
    '                        Exit Sub
    '                    End If
    '                End If
    '            End If

    '            '******************************
    '            'เช็คสถานะของคนที่ถูกโยกคิวว่ายังมีสถานะ Waithing อยู่หรือไม่
    '            Dim sql As String = ""
    '            'sql = "select status from TB_counter_queue where datediff(d,getdate(),service_date)=0 and item_id = '" & FixDB(obj.Cells(5).Value.ToString) & "' and customer_id = '" & FixDB(Grid.Tag) & "' and queue_no = '" & FixDB(TB_No) & "'"
    '            'ds = getDataset(sql)
    '            'If ds.Tables(0).Rows.Count = 0 Then
    '            '    If ds.Tables(0).Rows.Count = 0 Then
    '            '        MessageBox.Show("Information has been changed by another terminal !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            '        Exit Sub
    '            '    End If
    '            'End If
    '            '******************************


    '            sql = "select a.counter_id from TB_service_counter a inner join TB_service_counter_item b on a.counter_id = b.counter_id inner join TB_service_item c on b.item_id = c.item_id where a.doctor_room = '1' and c.item_id = '" & FixDB(obj.Cells(6).Value.ToString) & "' and a.counter_id ='" & FixDB(Grid.Tag) & "' group by a.counter_id"
    '            ds = getDataset(sql)
    '            If ds.Tables(0).Rows.Count = 0 Then
    '                MessageBox.Show("Service is not availabel for the customer", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If

    '            sql = "update TB_counter_queue set counter_id='" & Grid.Tag & "',service_time = getdate(),status = '1',hold = '' where status in (1,6) and datediff(d,getdate(),service_date)=0 and queue_no='" & TB_No & "' and counter_id ='" & CounterID & "'"
    '            executeSQL(sql)

    '            'Dim Temp As New DataGridViewRow
    '            'sql = "select a.queue_no as QueueNo,isnull(b.prename_desc,'') + ' ' + isnull(c.f_name,'') + ' ' + isnull(c.l_name,'') as Name,d.item_name as Service,datediff(n,a.service_time,getdate())as Time,a.counter_id,a.item_id from TB_counter_queue a inner join TB_customer c on a.customer_id = c.customer_id left outer join TB_prename b on c.prename_id = b.prename_id inner join TB_service_item d on a.item_id = d.item_id where a.counter_id ='" & Grid.Tag & "' and a.status = '1' and datediff(d,a.service_date,getdate())=0 order by a.service_time"
    '            sql = "select case when isnull(e.queue_no,'') = '' then 'No' else 'Tes' end as Appoint ,a.queue_no as QueueNo,isnull(b.prename_desc,'') + ' ' + isnull(c.f_name,'') + ' ' + isnull(c.l_name,'') as Name,d.item_name as Service,datediff(n,a.service_time,getdate())as Time,a.counter_id ,a.item_id,case when a.status = 1 then 'Waiting' else 'Hold' end as Status,a.status as sta from TB_counter_queue a inner join TB_customer c on a.customer_id = c.customer_id left outer join TB_prename b on c.prename_id = b.prename_id inner join TB_service_item d on a.item_id = d.item_id left join TB_customer_doctor e on datediff(d,a.service_date,s_date)=0 and a.customer_id = e.customer_id where a.counter_id ='" & Grid.Tag & "' and a.status in (1,6) and datediff(d,a.service_date,getdate())=0 order by a.service_time"
    '            ds = getDataset(sql)
    '            Grid.DataSource = ds.Tables(0)

    '            'sql = "select a.queue_no as QueueNo,isnull(b.prename_desc,'') + ' ' + isnull(c.f_name,'') + ' ' + isnull(c.l_name,'') as Name,d.item_name as Service,datediff(n,a.service_time,getdate())as Time,a.counter_id,a.item_id from TB_counter_queue a inner join TB_customer c on a.customer_id = c.customer_id left outer join TB_prename b on c.prename_id = b.prename_id inner join TB_service_item d on a.item_id = d.item_id where a.counter_id ='" & CounterID & "' and a.status = '1' and datediff(d,a.service_date,getdate())=0 order by a.service_time"
    '            sql = "select case when isnull(e.queue_no,'') = '' then 'No' else 'Tes' end as Appoint ,a.queue_no as QueueNo,isnull(b.prename_desc,'') + ' ' + isnull(c.f_name,'') + ' ' + isnull(c.l_name,'') as Name,d.item_name as Service,datediff(n,a.service_time,getdate())as Time,a.counter_id ,a.item_id,case when a.status = 1 then 'Waiting' else 'Hold' end as Status,a.status as sta from TB_counter_queue a inner join TB_customer c on a.customer_id = c.customer_id left outer join TB_prename b on c.prename_id = b.prename_id inner join TB_service_item d on a.item_id = d.item_id left join TB_customer_doctor e on datediff(d,a.service_date,s_date)=0 and a.customer_id = e.customer_id where a.counter_id ='" & CounterID & "' and a.status in (1,6) and datediff(d,a.service_date,getdate())=0 order by a.service_time"
    '            ds = getDataset(sql)
    '            obj.DataGridView.DataSource = ds.Tables(0)

    '        End If
    '    End If
    'End Sub

    'Private Sub DataGridView1_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
    '    Dim Grid As DataGridView = sender
    '    If e.RowIndex >= 0 Then
    '        Grid.DoDragDrop(Grid.Rows(e.RowIndex), DragDropEffects.Move)
    '    End If

    'End Sub

    ''*****************************************************

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    If Button1.Tag = "EXP" Then
    '        Me.WindowState = FormWindowState.Minimized
    '        Me.WindowState = FormWindowState.Maximized
    '        Button1.Text = "Window"
    '        Button1.Tag = "WND"
    '        Me.ControlBox = True
    '        Me.MdiParent = Nothing
    '    Else
    '        Me.WindowState = FormWindowState.Minimized
    '        Me.WindowState = FormWindowState.Maximized
    '        Button1.Text = "Expand"
    '        Button1.Tag = "EXP"
    '        Me.MdiParent = frmMain
    '    End If

    'End Sub

    'Private Sub DataGridView1_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs)
    '    CType(sender, DataGridView).BorderStyle = BorderStyle.None
    'End Sub

    'Private Sub DomainUp_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DomainUp.ValueChanged
    '    checkAutoRefresh()
    'End Sub


    'Private Sub frmViewDoctorService_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    '    Me.WindowState = FormWindowState.Maximized
    '    Me.StartPosition = FormStartPosition.CenterScreen
    '    Me.ControlBox = False
    'End Sub

    'Private Sub ButRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButRefresh.Click
    '    showdata(cbItem.SelectedValue)
    'End Sub


End Class