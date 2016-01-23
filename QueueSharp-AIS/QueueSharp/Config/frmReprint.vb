Option Explicit On
Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports QueueSharp.Org.Mentalis.Files
Public Class frmReprint

    Dim sql As String = ""
    Dim dt As New DataTable
    Dim queue As String = ""

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

            If TextQueue.Text = "" Then
                TextQueue.Focus()
                MessageBox.Show("กรุณากรอกหมายเลขคิว !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
        End If

        sql = "select queue_no,customer_id from TB_counter_queue where queue_no ='" & FixDB(TextQueue.Text) & "' and datediff(d,getdate(),service_date)=0 group by queue_no,customer_id"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                queue = TextQueue.Text.ToUpper
            Else
                MessageBox.Show("ไม่พบข้อมูล !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


        'อ่านค่าจาก File .ini
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim pnt As String = ini.ReadString("Print")
        Dim PrinterName As String = ini.ReadString("PrinterName")
        Dim copy As Int32 = 0
        '******************

        '********Print********
        If PrinterName <> "" Then
            copy = CInt(ini.ReadString("Copy"))
        End If
        For j As Int32 = 1 To copy
            printTicket(dt.Rows(0).Item("queue_no").ToString, dt.Rows(0).Item("customer_id").ToString, "", PrinterName)
        Next
        '*********************
        Me.Close()

       
       
    End Sub

    Private Sub frmReprint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextQueue.Focus()
    End Sub

End Class