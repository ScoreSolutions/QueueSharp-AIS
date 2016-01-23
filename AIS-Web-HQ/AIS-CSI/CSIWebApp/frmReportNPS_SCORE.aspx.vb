Imports System.Data
Imports System.Globalization
Imports System.IO
Partial Class CSIWebApp_frmReportNPS_SCORE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
        End If
    End Sub

    Sub ClearForm()
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        cmbServiceID.SelectedValue = "0"
        cmbShopID.SelectedValue = "0"
        cmbNetworkType.SelectedValue = ""
        ctlSelectFilterTemplate1.ClearSelectFilterID()
    End Sub


    Private Sub SetCombo()
        cmbShopID.SetItemList(Engine.Common.FunctionEng.GetActiveShopDDL, "shop_name", "id")

        Dim sEng As New Engine.Configuration.MasterENG
        cmbServiceID.SetItemList(sEng.GetServiceActiveList("1=1"), "item_name", "id")
        sEng = Nothing

        cmbNetworkType.SetItemList("All", "")
        cmbNetworkType.SetItemList("2G", "2G")
        cmbNetworkType.SetItemList("3G", "3G")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMessage.Text = ""
        If txtDateFrom.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date from !!!"
            Exit Sub
        End If
        If txtDateTo.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date to !!!"
            Exit Sub
        End If

        If txtDateFrom.DateValue > txtDateTo.DateValue Then

            lblMessage.Text = "คุณเลือก Date from มากกว่า Date to !!!"
            Exit Sub
        End If

        Dim para As String = ""
        para += "?ReportName=NPSSCORE"
        para += "&rnd=" & DateTime.Now.Millisecond

        If txtDateFrom.DateValue.Year <> 1 Then
            para += "&DateFrom=" & txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            para += "&DateTo=" & txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If cmbShopID.SelectedValue <> "0" Then
            para += "&ShopID=" & cmbShopID.SelectedValue
          
        End If
        If cmbServiceID.SelectedValue <> "0" Then
            para += "&ServiceID=" & cmbServiceID.SelectedValue
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            para += "&TemplateID=" & SelectTemplate
        End If

        para += "&Status=2" 'Complete

        If cmbNetworkType.SelectedValue <> "" Then
            para += "&NetworkType=" & cmbNetworkType.SelectedValue
        End If

        Dim scr As String = "window.open('../CSIWebApp/frmReportPreview.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub


    Protected Sub ctlSelectFilterTemplate1_Search(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlSelectFilterTemplate1.Search
        lblMessage.Text = ""
        If txtDateFrom.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date from !!!"
            Exit Sub
        End If
        If txtDateTo.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date to !!!"
            Exit Sub
        End If

        If txtDateFrom.DateValue > txtDateTo.DateValue Then
            lblMessage.Text = "คุณเลือก Date from มากกว่า Date to !!!"
            Exit Sub
        End If

        If txtDateFrom.DateValue.Year <> 1 Then
            ctlSelectFilterTemplate1.DateFrom = txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            ctlSelectFilterTemplate1.DateTo = txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
    End Sub

    Protected Sub btnSearch0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        ClearForm()
    End Sub
End Class
