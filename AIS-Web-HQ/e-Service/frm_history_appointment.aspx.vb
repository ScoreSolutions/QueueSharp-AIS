Imports System
Imports System.Data
Imports System.Globalization
Imports System.Drawing

Partial Class frm_history_appointment
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        MasterPageFile = cPara.MasterPage
        cPara = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            txtNetworkType.Text = cPara.NetworkType
            txtPreferLang.Text = cPara.PreferLang
            bindObj(cPara)
            BindTooltip(cPara)
            If cPara.MobileNo <> "" Then
                BindActiveAppointment(cPara.MobileNo)
            End If
        End If
    End Sub

    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""

            Dim strEndName As String
            Select Case cPara.NetworkType.ToLower
                Case "pre"
                    strEndName = "-green.png"
                Case "post"
                    strEndName = "-yellow.png"
                Case "corp"
                    strEndName = "-blue.png"
                Case "3g"
                    strEndName = "-green.png"
                Case "3gpost"
                    strEndName = "-green.png"
            End Select
            img_tab1.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab1").Replace(".png", "-gray.png")
            img_tab2.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab2").Replace(".png", strEndName)
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString

            dgvdetail.HeaderStyle.Font.Name = globalFunction.GetStyleDropdown(cPara.NetworkType, "font")
            dgvdetail.HeaderStyle.Font.Size = globalFunction.GetStyleDropdown(cPara.NetworkType, "size")
            Dim strColor As String() = globalFunction.GetStyleDropdown(cPara.NetworkType, "colorgrid").Split(",")
            dgvdetail.HeaderStyle.BackColor = ColorTranslator.FromOle(RGB(CInt(strColor(0)), CInt(strColor(1)), CInt(strColor(2))))

            If cPara.NetworkType = "pre" Then
                Panel5.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "post" Then
                Panel5.BackImageUrl = "~/image/gsm/bg_2.png"
            ElseIf cPara.NetworkType = "corp" Then
                Panel5.BackImageUrl = "~/image/corp/bg_2.png"
            ElseIf cPara.NetworkType = "3g" Then
                Panel5.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "3gpost" Then
                Panel5.BackImageUrl = "~/image/3gpost/bg_2.png"
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_tab1.ToolTip = "นัดรับบริการ"
            img_tab2.ToolTip = "ประวัติการรับบริการ"
        Else
            img_tab1.ToolTip = "Appointment"
            img_tab2.ToolTip = "History"
        End If
    End Sub

    Private Sub BindActiveAppointment(ByVal MobileNo As String)
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            Dim dt As New DataTable
            Dim dtActiveAppointment As New DataTable
            Dim dr As DataRow
            dt.Columns.Add("mobile_no")
            dt.Columns.Add("service_name")
            dt.Columns.Add("shop_name")
            dt.Columns.Add("date_time")
            dt.Columns.Add("date_nostring")
            dt.Columns.Add("time_nostring")
            dt.Columns.Add("day_nostring")
            dt.Columns.Add("month_nostring")
            dt.Columns.Add("year_nostring")
            dt.Columns.Add("status")

            Dim e As New Engine.Appointment.AppointmentENG
            dtActiveAppointment = e.GetAppointmentHistory(MobileNo)
            e = Nothing
            If dtActiveAppointment.Rows.Count > 0 Then
                lbl_message1.Visible = True
                lbl_message1.Text = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message1"), dtActiveAppointment.Rows.Count)
                For row As Integer = 0 To dtActiveAppointment.Rows.Count - 1
                    dr = dt.NewRow
                    dr("mobile_no") = dtActiveAppointment.Rows(row).Item("mobile_no")
                    Dim appdt As Date = Convert.ToDateTime(dtActiveAppointment.Rows(row).Item("slot_datetime")) 'dtActiveAppointment.Rows(row).Item("slot_date") & " " & dtActiveAppointment.Rows(row).Item("slot_time")
                    If cPara.PreferLang = "Thai" Then
                        dr("date_time") = "วัน" & appdt.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " ที่  " & appdt.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " " & appdt.ToString("HH.mm", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " น."
                        dr("day_nostring") = appdt.Day
                        dr("month_nostring") = appdt.Month
                        dr("year_nostring") = appdt.Year
                        dr("date_nostring") = appdt.Date
                        dr("time_nostring") = appdt.TimeOfDay
                        dr("service_name") = dtActiveAppointment.Rows(row).Item("service_name_th")
                        dr("shop_name") = dtActiveAppointment.Rows(row).Item("shop_name_th")
                    Else
                        dr("date_time") = appdt.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " " & appdt.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " " & appdt.ToString("HH.mm", CultureInfo.GetCultureInfo(cPara.CurrentLang))
                        dr("day_nostring") = appdt.Day
                        dr("month_nostring") = appdt.Month
                        dr("year_nostring") = appdt.Year
                        dr("date_nostring") = appdt.Date
                        dr("time_nostring") = appdt.TimeOfDay
                        dr("service_name") = dtActiveAppointment.Rows(row).Item("service_name_en")
                        dr("shop_name") = dtActiveAppointment.Rows(row).Item("shop_name_en")
                    End If
                    dr("status") = dtActiveAppointment.Rows(row).Item("status")
                    dt.Rows.Add(dr)
                Next
            Else
                lbl_message1.Visible = False

            End If
            dt.DefaultView.Sort = "year_nostring DESC,month_nostring DESC,day_nostring DESC,time_nostring DESC"
            dgvdetail.DataSource = dt
            dgvdetail.DataBind()

            lbl_message1.Text = lbl_message1.Text.Replace("x", dgvdetail.Items.Count)
            dt.Dispose()
            dtActiveAppointment.Dispose()
            cPara = Nothing
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindActiveAppointment Error !!,Please Check Function :" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub dgvdetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvdetail.ItemCreated
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
        globalFunction.Type = ""
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.Header Then

            Dim lbl_hd_tel As Label = DirectCast(e.Item.FindControl("lbl_hd_tel"), Label)
            Dim lbl_hd_service As Label = DirectCast(e.Item.FindControl("lbl_hd_service"), Label)
            Dim lbl_hd_shop As Label = DirectCast(e.Item.FindControl("lbl_hd_shop"), Label)
            Dim lbl_hd_date As Label = DirectCast(e.Item.FindControl("lbl_hd_date"), Label)
            Dim lbl_hd_status As Label = DirectCast(e.Item.FindControl("lbl_hd_status"), Label)

            If lbl_hd_tel Is Nothing Then
                Exit Sub
            End If
            lbl_hd_tel.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_tel")
            lbl_hd_service.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_service")
            lbl_hd_shop.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_shop")
            lbl_hd_date.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_date")
            lbl_hd_status.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_status")
        End If
        cPara = Nothing
    End Sub

    Protected Sub img_tab1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab1.Click
        Response.Redirect("frm_DontReservStt.aspx")
    End Sub

    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

End Class
