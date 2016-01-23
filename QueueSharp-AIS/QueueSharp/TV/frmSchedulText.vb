Public Class frmSchedulText

    Private Sub frmSchedulText_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Private Sub frmSchedulText_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        LoadFile()
    End Sub

    Sub LoadFile()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select mon_m,mon_a,tue_m,tue_a,wed_m,wed_a,thu_m,thu_a,fri_m,fri_a from TV_Schedul_txt"
        dt = getDataTable(sql)

        txt_Sh_Mon_M.Text = dt.Rows(0).Item("mon_m").ToString
        txt_Sh_Thu_M.Text = dt.Rows(0).Item("tue_m").ToString
        txt_Sh_Wen_M.Text = dt.Rows(0).Item("wed_m").ToString
        txt_Sh_Thr_M.Text = dt.Rows(0).Item("thu_m").ToString
        txt_Sh_Fri_M.Text = dt.Rows(0).Item("fri_m").ToString

        txt_Sh_Mon_A.Text = dt.Rows(0).Item("mon_a").ToString
        txt_Sh_Thu_A.Text = dt.Rows(0).Item("tue_a").ToString
        txt_Sh_Wen_A.Text = dt.Rows(0).Item("wed_a").ToString
        txt_Sh_Thr_A.Text = dt.Rows(0).Item("thu_a").ToString
        txt_Sh_Fri_A.Text = dt.Rows(0).Item("fri_a").ToString

    End Sub




#Region "ช่วงเช้า"
    Private Sub btnSave_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_M.Click
        SaveFile_M()
    End Sub


    Sub SaveFile_M()
        Dim sql As String = ""
        sql = "update TV_Schedul_txt set mon_m = '" & FixDB(txt_Sh_Mon_M.Text) & "',tue_m = '" & FixDB(txt_Sh_Thu_M.Text) & "',wed_m = '" & FixDB(txt_Sh_Wen_M.Text) & "',thu_m = '" & FixDB(txt_Sh_Thr_M.Text) & "',fri_m = '" & FixDB(txt_Sh_Fri_M.Text) & "',update_by_M = " & myUser.user_id & ",update_date_M = getdate()"
        executeSQL(sql)
        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "เรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "ช่วงบ่าย"

    Private Sub btnSave_A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_A.Click
        SaveFile_A()
    End Sub

    Sub SaveFile_A()
        Dim sql As String = ""
        sql = "update TV_Schedul_txt set mon_a = '" & FixDB(txt_Sh_Mon_A.Text) & "',tue_a = '" & FixDB(txt_Sh_Thu_A.Text) & "',wed_a = '" & FixDB(txt_Sh_Wen_A.Text) & "',thu_a = '" & FixDB(txt_Sh_Thr_A.Text) & "',fri_a = '" & FixDB(txt_Sh_Fri_A.Text) & "',update_by_A = " & myUser.user_id & ",update_date_A = getdate()"
        executeSQL(sql)
        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "เรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region


End Class
