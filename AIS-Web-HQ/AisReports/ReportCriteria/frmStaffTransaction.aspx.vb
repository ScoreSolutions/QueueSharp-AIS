Imports System.Data

Partial Class ReportCriteria_frmStaffTransaction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim dt As DataTable = Engine.Reports.ReportsENG.getShopList()
            Dim dt As DataTable = Engine.Reports.ReportsENG.GetShopListByUser()
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i).Item("shop_name_en") = dt.Rows(i).Item("shop_name_en") & "" & "&nbsp;"
            Next
            If dt.Rows.Count > 0 Then
                chkShopId.DataTextField = "shop_name_en"
                chkShopId.DataValueField = "id"
                chkShopId.DataSource = dt
                chkShopId.DataBind()
            End If

        End If
    End Sub

    Private Function GetShopID() As String
        Dim tmpID As String = ""
        For i As Integer = 0 To chkShopId.Items.Count - 1
            If chkShopId.Items(i).Selected = True Then
                If tmpID = "" Then
                    tmpID = chkShopId.Items(i).Value
                Else
                    tmpID += "," & chkShopId.Items(i).Value
                End If
            End If
        Next

        Return tmpID
    End Function

    Private Sub chkAllShop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllShop.CheckedChanged
        If chkAllShop.Checked = True Then
            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = True
            Next
        Else
            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = False
            Next
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim para As String = ""
        Dim ShopID As String = GetShopID()
        If ShopID.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Shop ที่ต้องการแสดงรายงาน !!')</script>", False)
            Exit Sub
        End If

        If ctlByDate1.DateFrom.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date From !!')</script>", False)
            Exit Sub
        End If

        If ctlByDate1.DateTo.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date To !!')</script>", False)
            Exit Sub
        End If
        If ctlByDate1.DateFrom > ctlByDate1.DateTo Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Date From มากกว่า Date To !!')</script>", False)
            Exit Sub
        End If

        para += "?ReportName=ByDate"
        para += "&ShopID=" & ShopID
        para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
        para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
        para += "&rnd=" & DateTime.Now.Millisecond

        Config.SaveTransLog("เรียกดูรายงาน : Staff Transaction Report เงื่อนไข :" & para, "AisReports.ReportCriteria_frmStaffAttendance.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repStaffTransaction.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

End Class
