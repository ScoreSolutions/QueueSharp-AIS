Public Class frmMDSetting

    Dim dt_data As New DataTable

    
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '[config_name]
        '[config_value]
        '[config_desc]
        Dim config_name As String = ""
        Dim config_value As String = ""
        Dim config_desc As String = ""
        Dim New_ As Boolean = False
        Dim sql As String = ""
        Dim dt As New DataTable
       
        config_name = "m_disable"
        config_value = nud_m_disable.Value.ToString
        config_desc = "(Maindisplay) Disable Waiting time is Over"

        sql = "select * from TB_SETTING where config_name = '" & config_name & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count = 0 Then
            Dim id As Int32 = FindID("TB_SETTING")
            sql = "insert into TB_SETTING(id,config_name,config_value,config_desc,create_by,create_date) values(" & id & ",'" & config_name & "','" & config_value & "','" & config_desc & "'," & myUser.user_id & ",getdate())"
        Else
            sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"
        End If
        executeSQL(sql)

        MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub frmSetting_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from TB_SETTING where config_name = 'm_disable'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            nud_m_disable.Value = dt.Rows(0).Item("config_value").ToString
        End If
    End Sub

End Class