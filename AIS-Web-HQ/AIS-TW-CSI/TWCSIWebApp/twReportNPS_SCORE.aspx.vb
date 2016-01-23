Imports System.Data
Imports System.Globalization
Imports System.IO
Partial Class TWCSIWebApp_twReportNPS_SCORE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
        End If
    End Sub

    Private Sub SetCombo()
        Dim sEng As New Engine.Configuration.MasterENG
        'cmbLocationCode.SetItemList(sEng.GetTWShopList(" and 1=1"), "location_name_en", "location_code")
        cmbRegion.SetItemList(sEng.GetTWRegionAllList(), "name", "code")
        cmbSFFOrderTypeID.SetItemList(sEng.GetTWSffOrderTypeList("1=1"), "order_type_name", "id")
        sEng = Nothing

        'cmbStatus.SetItemList("All", "0")
        'cmbStatus.SetItemList("Complete", "2")
        'cmbStatus.SetItemList("Incomplete", "1,3")

        cmbNetworkType.SetItemList("All", "")
        cmbNetworkType.SetItemList("2G", "2G")
        cmbNetworkType.SetItemList("3G", "3G")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMessage.Text = ""
        If txtDateFrom.DateValue.Year = "1" Then
            'Config.SetAlert("กรุณาเลือก Date from !!!", Me, txtDateFrom.ClientID)
            lblMessage.Text = "กรุณาเลือก Date from !!!"
            Exit Sub
        End If
        If txtDateTo.DateValue.Year = "1" Then
            'Config.SetAlert("กรุณาเลือก Date to !!!", Me, txtDateTo.ClientID)
            lblMessage.Text = "กรุณาเลือก Date to !!!"
            Exit Sub
        End If

        If txtDateFrom.DateValue > txtDateTo.DateValue Then
            'Config.SetAlert("คุณเลือก Date From มากกว่า Date To !!!", Me)
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
        If cmbRegion.SelectedValue <> "0" Then
            para += "&RegionCode=" & cmbRegion.SelectedValue

            If cmbProvince.SelectedValue <> "0" Then
                para += "&ProvinceCode=" & cmbProvince.SelectedValue

                If cmbLocationCode.SelectedValue <> "0" Then
                    para += "&LocationCode=" & cmbLocationCode.SelectedValue
                End If
            End If
        End If
        If cmbSFFOrderTypeID.SelectedValue <> "0" Then
            para += "&OrderTypeID=" & cmbSFFOrderTypeID.SelectedValue
        End If
        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            para += "&TemplateID=" & SelectTemplate
        End If
        'If cmbStatus.SelectedValue <> "0" Then
        para += "&Status=2" 'Complete
        'End If
        If cmbNetworkType.SelectedValue <> "" Then
            para += "&NetworkType=" & cmbNetworkType.SelectedValue
        End If

        Dim scr As String = "window.open('../TWCSIWebApp/twReportPreview.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Dim sEng As New Engine.Configuration.MasterENG
        cmbProvince.SetItemList(sEng.GetTWProvinceList(cmbRegion.SelectedValue), "name", "code")

        Dim dt As New DataTable
        dt = sEng.GetTWShopList(" and province_code='" & cmbProvince.SelectedValue & "'", "")
        dt.DefaultView.Sort = "location_code"
        dt = dt.DefaultView.ToTable
        cmbLocationCode.SetItemList(dt, "location_name_ddl", "location_code")
        sEng = Nothing
        dt.Dispose()
    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvince.SelectedIndexChanged
        Dim sEng As New Engine.Configuration.MasterENG

        Dim dt As New DataTable
        dt = sEng.GetTWShopList(" and province_code='" & cmbProvince.SelectedValue & "'", "")
        dt.DefaultView.Sort = "location_code"
        dt = dt.DefaultView.ToTable
        cmbLocationCode.SetItemList(dt, "location_name_ddl", "location_code")
        sEng = Nothing
        dt.Dispose()
    End Sub

    Protected Sub ctlSelectFilterTemplate1_Search(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlSelectFilterTemplate1.Search
        lblMessage.Text = ""
        If txtDateFrom.DateValue.Year = "1" Then
            'Config.SetAlert("กรุณาเลือก Date from !!!", Me, txtDateFrom.ClientID)
            lblMessage.Text = "กรุณาเลือก Date from !!!"
            Exit Sub
        End If
        If txtDateTo.DateValue.Year = "1" Then
            'Config.SetAlert("กรุณาเลือก Date to !!!", Me, txtDateTo.ClientID)
            lblMessage.Text = "กรุณาเลือก Date to !!!"
            Exit Sub
        End If

        If txtDateFrom.DateValue > txtDateTo.DateValue Then
            'Config.SetAlert("คุณเลือก Date From มากกว่า Date To !!!", Me)
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

    Sub ClearForm()
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        cmbSFFOrderTypeID.SelectedValue = "0"
        cmbNetworkType.SelectedValue = ""
        cmbRegion.SelectedValue = "0"
        cmbProvince.SelectedValue = "0"
        cmbLocationCode.SelectedValue = "0"
        ctlSelectFilterTemplate1.ClearSelectFilterID()
    End Sub

    Protected Sub btnSearch0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        ClearForm()
    End Sub
End Class
