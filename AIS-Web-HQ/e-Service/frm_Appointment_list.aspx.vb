Imports Engine.Appointment
Imports System
Imports System.Data
Imports System.Globalization
Imports System.DateTime
Imports System.Data.OleDb
Imports System.Drawing

Partial Class frm_Appointment_list
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction
    'Dim objConn As New OleDbConnection(ConfigurationManager.ConnectionStrings("QisDB").ConnectionString)

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
            BindActiveAppointment(cPara)
            cPara = Nothing
        End If
    End Sub

    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""
            img_tab1.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab1")
            img_tab2.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab2")
            'lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_message2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message2")
            'lbl_message3.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message3")
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString
            img_tab1.Enabled = False
            'img_tab2.Enabled = False
            dgvdetail.HeaderStyle.Font.Name = globalFunction.GetStyleDropdown(cPara.NetworkType, "font")
            dgvdetail.HeaderStyle.Font.Size = globalFunction.GetStyleDropdown(cPara.NetworkType, "size")
            dgvdetail.HeaderStyle.BackColor = ColorTranslator.FromHtml(globalFunction.GetStyleDropdown(cPara.NetworkType, "color"))

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

    Private Sub BindActiveAppointment(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        'Try
        Dim dt As New DataTable
        Dim dtapp As New DataTable
        Dim dr As DataRow
        dt.Columns.Add("tel")
        dt.Columns.Add("service")
        dt.Columns.Add("servicenm")
        dt.Columns.Add("shop_id")
        dt.Columns.Add("shop")
        dt.Columns.Add("appdt")
        dt.Columns.Add("date")
        Dim e As New AppointmentENG
        If e.GetActiveAppointment(cPara.MobileNo) = True Then
            dtapp = e.GetAppointmentDesc(cPara.MobileNo)
        End If

        If dtapp.Rows.Count > 0 Then
            lbl_message1.Text = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message1"), dtapp.Rows.Count)
            lbl_message3.Text = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message3"), e.MaxEditAppointmentHour(dtapp.Rows(0)("shop_id")))
            Dim dtshop As New DataTable
            dtshop = Engine.Common.FunctionEng.GetActiveShop()
            Dim dtservice As New DataTable

            For row As Integer = 0 To dtapp.Rows.Count - 1
                Dim strservice As String = ""
                Dim strserviceid As String = String.Empty
                dr = dt.NewRow
                dr(0) = cPara.MobileNo
                If dtshop.Rows.Count > 0 Then
                    For rowshop As Integer = 0 To dtshop.Rows.Count - 1
                        If dtshop.Rows(rowshop).Item("id").ToString = dtapp.Rows(row).Item("shop_id").ToString Then
                            Dim arrservice As Array
                            arrservice = dtapp.Rows(row).Item("service_id").ToString.Split(",")

                            For id As Integer = 0 To UBound(arrservice)
                                dtservice = globalFunction.RetriveItem_Applist(arrservice(id), dtshop.Rows(rowshop).Item("shop_db_name").ToString, _
                                            dtshop.Rows(rowshop).Item("shop_db_userid").ToString, dtshop.Rows(rowshop).Item("shop_db_pwd").ToString, _
                                            dtshop.Rows(rowshop).Item("shop_db_server").ToString)

                                If dtservice.Rows.Count > 0 Then
                                    If cPara.PreferLang = "Thai" Then
                                        strservice &= dtservice.Rows(0).Item("Item_name_th") & ","
                                    Else
                                        strservice &= dtservice.Rows(0).Item("Item_name") & ","
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If

                dr(1) = dtapp.Rows(row).Item("service_id").ToString
                dr(2) = IIf(strservice = "", "", strservice.Substring(0, strservice.Length - 1))
                dr(3) = dtapp.Rows(row).Item("shop_id").ToString

                If dtshop.Rows.Count > 0 Then
                    For rowshop As Integer = 0 To dtshop.Rows.Count - 1
                        If dtshop.Rows(rowshop).Item("id").ToString = dtapp.Rows(row).Item("shop_id").ToString Then
                            If cPara.PreferLang = "Thai" Then
                                dr(4) = dtshop.Rows(rowshop).Item("shop_name_th").ToString
                            Else
                                dr(4) = dtshop.Rows(rowshop).Item("shop_name_en").ToString
                            End If
                            Exit For
                        Else
                            dr(4) = ""
                        End If
                    Next
                End If

                Dim slot_date() As String
                slot_date = dtapp.Rows(row).Item("slot_date").ToString.Split("/")

                Dim appdt As New Date(slot_date(2), slot_date(1), slot_date(0))
                Dim apptm As String = dtapp.Rows(row).Item("slot_time")
                dr(5) = appdt.ToString("yyyy-MM-dd") & " " & apptm
                If cPara.PreferLang = "Thai" Then
                    dr(6) = "วัน" & appdt.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " ที่  " & appdt.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " " & apptm & " น."
                Else
                    dr(6) = appdt.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & "  " & appdt.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang)) & " " & apptm
                End If
                dt.Rows.Add(dr)
            Next
        End If
        e = Nothing

        dgvdetail.DataSource = dt
        dgvdetail.DataBind()
        MaxEditAppointmentHour(dtapp)

        dtapp.Dispose()
        dt.Dispose()
    End Sub

    Private Sub MaxEditAppointmentHour(ByVal dt As DataTable)
        Dim e As New Engine.Appointment.AppointmentENG
        Dim dgi As DataGridItem
        For Each dgi In dgvdetail.Items
            Dim lbl_shop_id As Label = dgi.FindControl("lbl_shop_id")
            Dim MaxHour As Integer = e.MaxEditAppointmentHour(lbl_shop_id.Text)
            Dim MaxTime As DateTime = DateAdd(DateInterval.Hour, MaxHour, DateTime.Now)

            Dim img_cancel As ImageButton = dgi.FindControl("img_cancel")
            Dim img_edit As ImageButton = dgi.FindControl("img_edit")
            If Convert.ToDateTime(dt.Rows(0)("slot_datetime")) < MaxTime Then
                img_cancel.Visible = False
                img_edit.Visible = False
            End If
            If dt.Rows(0)("status").ToString <> "" Then  'กรณีการจองที่ Confirm แล้ว Status จะเท่ากับค่าว่าง
                img_cancel.Visible = False
                img_edit.Visible = False
            End If
        Next
        e = Nothing
    End Sub

    Protected Sub dgvdetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvdetail.ItemCreated
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
        globalFunction.Type = ""

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim img_edit As ImageButton = DirectCast(e.Item.FindControl("img_edit"), ImageButton)
            Dim img_cancel As ImageButton = DirectCast(e.Item.FindControl("img_cancel"), ImageButton)
            img_edit.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_edit")
            img_cancel.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_cancel")
            If cPara.PreferLang.ToLower = "thai" Then
                img_edit.ToolTip = "แก้ไข"
                img_cancel.ToolTip = "ยกเลิก"
            Else
                img_edit.ToolTip = "Edit"
                img_cancel.ToolTip = "Cancel"
            End If
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.Header Then

            Dim lbl_hd_tel As Label = DirectCast(e.Item.FindControl("lbl_hd_tel"), Label)
            Dim lbl_hd_service As Label = DirectCast(e.Item.FindControl("lbl_hd_service"), Label)
            Dim lbl_hd_shop As Label = DirectCast(e.Item.FindControl("lbl_hd_shop"), Label)
            Dim lbl_hd_date As Label = DirectCast(e.Item.FindControl("lbl_hd_date"), Label)
            Dim lbl_hd_edit As Label = DirectCast(e.Item.FindControl("lbl_hd_edit"), Label)
            Dim lbl_hd_cancel As Label = DirectCast(e.Item.FindControl("lbl_hd_cancel"), Label)
            If lbl_hd_tel Is Nothing Then
                Exit Sub
            End If
            lbl_hd_tel.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_tel")
            lbl_hd_service.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_service")
            lbl_hd_shop.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_shop")
            lbl_hd_date.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_date")
            lbl_hd_edit.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_edit")
            lbl_hd_cancel.Text = globalFunction.GetText(cPara.PreferLang, "lbl_hd_cancel")
        End If
        cPara = Nothing
    End Sub

    Protected Sub dgvdetail_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_service As Label = DirectCast(e.Item.FindControl("lbl_service"), Label)
            Dim lbl_shop As Label = DirectCast(e.Item.FindControl("lbl_shop"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_appdt As Label = DirectCast(e.Item.FindControl("lbl_appdt"), Label)
            Session("Edit") = "True"
            Response.Redirect("frm_Appointment_mnt.aspx?&shop_id=" & lbl_shop_id.Text & "|" & lbl_shop.Text & "&service=" & lbl_service.Text & "&appdt=" & lbl_appdt.Text)
        End If
    End Sub



    Protected Sub dgvdetail_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.CancelCommand
        If e.CommandName = "Cancel" Then
            Dim lbl_service As Label = DirectCast(e.Item.FindControl("lbl_service"), Label)
            Dim lbl_shop As Label = DirectCast(e.Item.FindControl("lbl_shop"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_appdt As Label = DirectCast(e.Item.FindControl("lbl_appdt"), Label)

            Response.Redirect("frm_delete_appointment.aspx?&shop_id=" & lbl_shop_id.Text & "|" & lbl_shop.Text & "&service=" & lbl_service.Text & "&appdt=" & lbl_appdt.Text)
        End If

    End Sub

    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

    Protected Sub img_tab1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab1.Click
        Response.Redirect("frm_Appointment_mnt.aspx")
    End Sub
End Class
