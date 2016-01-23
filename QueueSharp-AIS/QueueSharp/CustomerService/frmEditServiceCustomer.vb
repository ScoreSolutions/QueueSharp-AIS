Imports System.Data.SqlClient

Public Class frmEditServiceCustomer

    Dim dt_cus As New DataTable
    Dim dt_ser As New DataTable
    Dim sql As String = ""

    Sub LoadService()
        sql = "select x.id,x.item_id,y.item_name,queue_no,z.status_name as status_name,x.status from TB_COUNTER_QUEUE x left join TB_ITEM y on x.item_id = y.id left join TB_STATUS z on x.status = z.id where DATEDIFF(D,GETDATE(),service_date) = 0"
        dt_ser = getDataTable(sql)
        GridItem.DataSource = dt_ser
    End Sub

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        TextFilter.Focus()
        LoadService()
        sql = "select queue_no,customer_id,y.customertype_name,x.customertype_id  from TB_COUNTER_QUEUE x left join TB_CUSTOMERTYPE y on x.customertype_id = y.id where DATEDIFF(D,GETDATE(),service_date) = 0 group by queue_no,customer_id,y.customertype_name,x.customertype_id order by queue_no"
        dt_cus = getDataTable(sql)
        GridCustomer.DataSource = dt_cus

    End Sub

    Private Sub GridCustomer_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCustomer.SelectionChanged
        Try
            dt_ser.DefaultView.RowFilter = "queue_no = '" & GridCustomer.SelectedRows(0).Cells("queue_no").Value & "'"
        Catch ex As Exception : End Try
    End Sub

    Private Sub GridService_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridItem.CellDoubleClick
        If GridItem.Rows.Count > 0 Then
            If GridItem.SelectedRows(0).Cells("status").Value.ToString <> "4" Or GridItem.SelectedRows(0).Cells("status").Value.ToString <> "6" Then
                Dim f As New frmEditService
                f.lblCustomerID.Text = GridCustomer.SelectedRows(0).Cells("customer_id").Value.ToString
                f.lblQueue.Text = GridCustomer.SelectedRows(0).Cells("queue_no").Value.ToString
                f.lblService.Text = GridItem.SelectedRows(0).Cells("item_name").Value.ToString
                f.lblqcTB_id.Text = GridItem.SelectedRows(0).Cells("id").Value.ToString
                f.StatusID = GridItem.SelectedRows(0).Cells("status").Value.ToString
                f.itemID = GridItem.SelectedRows(0).Cells("item_id").Value.ToString
                If f.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                    LoadService()
                    dt_ser.DefaultView.RowFilter = "queue_no = '" & GridCustomer.SelectedRows(0).Cells("queue_no").Value & "'"
                End If
            Else
                MessageBox.Show("Can't edit this service", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub frmEdit_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.ControlBox = False
        Else
            Me.ControlBox = True
        End If
    End Sub

    Private Sub TextFilter_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextFilter.KeyUp
        Try
            dt_cus.DefaultView.RowFilter = "queue_no like '%" & FixDB(TextFilter.Text) & "%' or customer_id like '%" & FixDB(TextFilter.Text) & "%'"
            If dt_cus.DefaultView.Count = 0 Then
                dt_ser.DefaultView.RowFilter = "queue_no = ''"
            End If
        Catch ex As Exception : End Try
    End Sub

End Class