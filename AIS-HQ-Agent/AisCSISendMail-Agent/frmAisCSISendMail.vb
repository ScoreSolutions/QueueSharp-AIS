Public Class frmAisCSISendMail

    Private Sub frmAisCSISendMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetActiveShop
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "shop_name_en"
            dt = dt.DefaultView.ToTable

            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("shop_name_en") = "All Shop"
            dt.Rows.InsertAt(dr, 0)

            cmbShop.ValueMember = "id"
            cmbShop.DisplayMember = "shop_name_en"
            cmbShop.DataSource = dt
        End If

        If My.Application.CommandLineArgs.Count > 0 Then
            dt.DefaultView.RowFilter = "id not in (0)"
            dt = dt.DefaultView.ToTable
            If dt.Rows.Count > 0 Then
                Dim eng As New Engine.CSISurveyENG
                For Each dr As DataRow In dt.Rows
                    eng.CSISendEmail(DateTime.Now, dr("id"))
                Next
                eng = Nothing
            End If
            If txtLog.Text.Trim = "" Then
                dt = Nothing
                End
            End If
        End If
        dt = Nothing
    End Sub

    Private Sub frmAisCSISendMail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS CSI Send E-Mail Agent V" & getMyVersion()
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Dim eng As New Engine.CSISurveyENG
        eng.SetTextLog(txtLog)

        Dim CurrDate As DateTime = dtpST.Value
        Do
            If cmbShop.SelectedValue <> "0" Then
                eng.CSISendEmail(CurrDate, cmbShop.SelectedValue)
            Else
                Dim dt As New DataTable
                dt = Engine.Common.FunctionEng.GetActiveShop
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        eng.CSISendEmail(CurrDate, dr("id"))
                    Next
                End If
                dt = Nothing
            End If

            CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        Loop While CurrDate <= dtpET.Value

        If txtLog.Text.Trim = "" Then
            MessageBox.Show("Send Email Complete!!!")
        Else
            MessageBox.Show(txtLog.Text)
        End If
        eng = Nothing
    End Sub
End Class
