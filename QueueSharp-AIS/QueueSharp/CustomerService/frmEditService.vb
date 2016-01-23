Imports System.Data.SqlClient
Public Class frmEditService
    
    Public StatusID As String = ""
    Public itemID As String = ""

    Sub showstatus()
        Dim dt As New DataTable
        Dim sql As String = ""

        sql = "select * from TB_STATUS where id in (1,3,5,8)"
        dt = getDataTable(Sql)
        With cbStatus
            .DataSource = dt
            .DisplayMember = "status_name"
            .ValueMember = "id"
        End With
        cbStatus.SelectedValue = StatusID
    End Sub

    Private Sub ButCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub ButOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cbStatus.SelectedValue <> StatusID Then
            Dim dt As New DataTable
            Dim sql As String = ""
            sql = "update TB_counter_queue set status = " & cbStatus.SelectedValue & " where id = " & lblqcTB_id.Text
            executeSQL(sql)

            Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
            Dim vDateNow As String = FixDateTime(FindDateNow)
            InsertLog(lblQueue.Text, lblCustomerID.Text, myUser.user_id, itemID, 0, cbStatus.SelectedValue, "", Nothing, vDateNow, CurrDB)

            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Else
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
        Me.Close()
    End Sub

    Private Sub frmEditService_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        showstatus()
    End Sub

End Class