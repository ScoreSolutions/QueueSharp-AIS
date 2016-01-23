Public Class frmUpdateCustomer

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Timer1.Enabled = True
        Dim trans As New Utilities.TransactionDB
        Utilities.SqlDB.ExecuteNonQuery("delete from tb_customer", trans.Trans)
        trans.CommitTransaction()

        trans = New Utilities.TransactionDB
        Dim dt As DataTable = Utilities.SqlDB.ExecuteTable("select  * from tb_customer_test", trans.Trans)
        trans.CommitTransaction()

        If dt.Rows.Count > 0 Then
            Dim maxRow As Long = dt.Rows.Count
            pgb1.Maximum = maxRow
            For i As Integer = 0 To dt.Rows.Count - 1
                trans = New Utilities.TransactionDB
                Dim lnq As New Utilities.TbCustomerCenLinqDB
                lnq.MOBILE_NO = IIf(Convert.IsDBNull(dt.Rows(i)("mobile_no")) = False, dt.Rows(i)("mobile_no"), "")
                lnq.TITLE_NAME = IIf(Convert.IsDBNull(dt.Rows(i)("x_title")) = False, dt.Rows(i)("x_title"), "")
                lnq.F_NAME = IIf(Convert.IsDBNull(dt.Rows(i)("fst_name")) = False, dt.Rows(i)("fst_name"), "")
                lnq.L_NAME = IIf(Convert.IsDBNull(dt.Rows(i)("last_name")) = False, dt.Rows(i)("last_name"), "")
                lnq.EMAIL = IIf(Convert.IsDBNull(dt.Rows(i)("email_addr")) = False, dt.Rows(i)("email_addr"), "")
                lnq.PREFER_LANG = IIf(Convert.IsDBNull(dt.Rows(i)("pre_lang")) = False, dt.Rows(i)("pre_lang"), "")
                lnq.SEGMENT_LEVEL = IIf(Convert.IsDBNull(dt.Rows(i)("segment")) = False, dt.Rows(i)("segment"), "")
                lnq.BIRTH_DATE = IIf(Convert.IsDBNull(dt.Rows(i)("bdate")) = False, dt.Rows(i)("bdate"), "")
                lnq.MOBILE_STATUS = IIf(Convert.IsDBNull(dt.Rows(i)("status")) = False, dt.Rows(i)("status"), "")
                lnq.CATEGORY = IIf(Convert.IsDBNull(dt.Rows(i)("category")) = False, dt.Rows(i)("category"), "")
                lnq.ACCOUNT_BALANCE = IIf(Convert.IsDBNull(dt.Rows(i)("acc_balance")) = False, dt.Rows(i)("acc_balance"), "")
                lnq.CONTACT_CLASS = IIf(Convert.IsDBNull(dt.Rows(i)("contact_class")) = False, dt.Rows(i)("contact_class"), "")
                lnq.SERVICE_YEAR = IIf(Convert.IsDBNull(dt.Rows(i)("service_year")) = False, dt.Rows(i)("service_year"), "")
                lnq.CHUM_SCORE = IIf(Convert.IsDBNull(dt.Rows(i)("churn_score")) = False, dt.Rows(i)("churn_score"), "")
                lnq.CAMPAIGN_CODE = IIf(Convert.IsDBNull(dt.Rows(i)("camp_code")) = False, dt.Rows(i)("camp_code"), "")
                lnq.CAMPAIGN_NAME = IIf(Convert.IsDBNull(dt.Rows(i)("camp_name")) = False, dt.Rows(i)("camp_name"), "")
                lnq.NETWORK_TYPE = IIf(Convert.IsDBNull(dt.Rows(i)("network type")) = False, dt.Rows(i)("network type"), "")
                If lnq.InsertData("INITIAL", trans.Trans) = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If

                pgb1.Value = i + 1
                lblProgress.Text = "Row " & (i + 1) & " of " & (maxRow) & " Rows" & vbNewLine
                lblProgress.Text += Math.Round((((i + 1) * 100) / maxRow), 4) & "%"
                Application.DoEvents()
            Next
        End If
        Timer1.Enabled = False
        MessageBox.Show("Import Complete")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim strTime() As String = Split(lblTime.Text, ":")
        Dim strMin As Integer = Val(strTime(1))
        Dim strHour As Integer = Val(strTime(0))

        If strMin = 59 Then
            strMin = 0
            strHour += 1
        Else
            strMin += 1
        End If

        lblTime.Text = strHour.ToString.PadLeft(2, "0") & ":" & strMin.ToString.PadLeft(2, "0")
    End Sub
End Class