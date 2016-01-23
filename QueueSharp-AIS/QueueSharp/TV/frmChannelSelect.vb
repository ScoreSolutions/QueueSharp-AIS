Public Class frmChannelSelect

    Private Sub frmChannelSelect_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Private Sub frmChannelSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        AddChannel()
    End Sub

    Sub AddChannel()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,name,channel from TV_Channel where active_status = 1 order by name"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim Font As Font = New Font("Tahoma", 15, FontStyle.Bold, GraphicsUnit.Pixel)
                Dim btn As New Button
                With btn
                    .Width = 200
                    .Height = 65
                    .Name = dt.Rows(i).Item("id").ToString
                    .Text = dt.Rows(i).Item("name").ToString
                    .Tag = dt.Rows(i).Item("channel").ToString
                    .Font = Font
                    .ForeColor = Color.Navy
                    .AutoSize = True
                    .FlatStyle = FlatStyle.Flat
                    .BackColor = Color.White
                    .Dock = DockStyle.Fill
                End With
                TLP.Controls.Add(btn)
                AddHandler btn.Click, AddressOf CheckCH
            Next

        End If
    End Sub

    Private Sub CheckCH(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Button = Sender
        If MessageBox.Show("ท่านต้องการเปลี่ยนช่องสัญญาณโทรทัศน์เป็น " & btn.Text & " ใช่หรือไม่ ?", "ยันยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim sql As String = ""
                sql = "update TV_Channel set show = 0"
                executeSQL(sql)

                sql = "update TV_Channel set show = 1 where id = " & btn.Name
                executeSQL(sql)
            Catch ex As Exception : End Try
        End If
    End Sub
End Class