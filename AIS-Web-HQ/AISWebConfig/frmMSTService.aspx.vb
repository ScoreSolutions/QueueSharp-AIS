Imports Engine.Configuration
Imports System.Data
Imports System.Drawing

Partial Class frmMSTService
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetServiceList()
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")

            txt_item_code.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_item_name_english.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_item_name_thai.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_appointment_queue_no_min.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_appointment_queue_no_max.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_queue.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_item_order.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_standard_handing_time.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_standard_waiting_time.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")

            Config.SaveTransLog("คลิกเมนู : Central Setup >> Service", "AisWebConfig.frmMSTService.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub SetServiceList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetServiceAllList("1=1")
        If dt.Rows.Count > 0 Then
            dgvService.DataSource = dt
            dgvService.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbItemCenParaDB
            p.ID = txt_id.Text
            p.ITEM_CODE = txt_item_code.Text.Trim
            p.ITEM_NAME = txt_item_name_english.Text.Trim
            p.ITEM_NAME_TH = txt_item_name_thai.Text.Trim
            p.ITEM_TIME = txt_standard_handing_time.Text.Trim
            p.ITEM_WAIT = txt_standard_waiting_time.Text.Trim
            p.ITEM_ORDER = txt_item_order.Text.Trim
            p.TXT_QUEUE = txt_queue.Text.Trim

            Try
                Dim hexcolor As Color = System.Drawing.ColorTranslator.FromHtml("#" & txt_colorcode.Text)
                p.COLOR = hexcolor.ToArgb.ToString
            Catch ex As Exception

            End Try

            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If

            If chk_vasily.Checked = True Then
                p.BRAND_NAME = "Y"
            Else
                p.BRAND_NAME = "N"
            End If

            p.APP_MIN_QUEUE = txt_appointment_queue_no_min.Text.Trim
            p.APP_MAX_QUEUE = txt_appointment_queue_no_max.Text.Trim

            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            Dim eng As New Engine.Configuration.MasterENG
            If eng.SaveMasterService(uPara.USERNAME, p) = True Then
                Config.ShowAlert("Save Complete.", Me)
                SetServiceList()
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If
            eng = Nothing

            Config.SaveTransLog("บันทึกข้อมูล : Central Setup >> Service", "AisWebConfig.frmMSTService.aspx.CmdSave_Click", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txt_item_code.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Item Code", Me, txt_item_code.ClientID)
        ElseIf txt_item_name_english.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Name in English", Me, txt_item_name_english.ClientID)
            ret = False
        ElseIf txt_item_name_thai.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Name in Thai", Me, txt_item_name_thai.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_min.Text.Trim = "" Then
            Config.ShowAlert("Please input Appointment Queue No Min", Me, txt_appointment_queue_no_min.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_max.Text.Trim = "" Then
            Config.ShowAlert("Please input Appointment Queue No Max", Me, txt_appointment_queue_no_max.ClientID)
            ret = False
        
        ElseIf txt_item_order.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Order", Me, txt_item_order.ClientID)
            ret = False
        ElseIf txt_queue.Text.Trim = "" Then
            Config.ShowAlert("Please input Text Queue", Me, txt_queue.ClientID)
            ret = False
        ElseIf txt_standard_handing_time.Text.Trim = "" Then
            Config.ShowAlert("Please input Standard Handling time", Me, txt_standard_handing_time.ClientID)
            ret = False
            'ElseIf txt_colorcode.Text = "" Then
            '    Config.ShowAlert("Please Select Color !!", Me, txt_colorcode.ClientID)
            '    ret = False
        ElseIf txt_standard_waiting_time.Text.Trim = "" Then
            Config.ShowAlert("Please input Standard Waiting time", Me, txt_standard_waiting_time.ClientID)
            ret = False
        ElseIf txt_standard_waiting_time.Text < 0 Then
            Config.ShowAlert("Standard Waiting time < 0,Please Try Again !!", Me, txt_standard_waiting_time.ClientID)
            ret = False
        ElseIf txt_standard_handing_time.Text < 0 Then
            Config.ShowAlert("Standard Handling time < 0,Please Try Again !!'", Me, txt_standard_handing_time.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_min.Text >= txt_appointment_queue_no_max.Text Then
            Config.ShowAlert("Max Appointment Queue No < Min Appointment Queue No ,Please Try Again !!", Me, txt_appointment_queue_no_min.ClientID)
            ret = False
        ElseIf txt_item_order.Text < 0 Then
            Config.ShowAlert("Item Order < 0,Please Try Again !!", Me, txt_item_order.ClientID)
            ret = False
        Else
            Dim eng As New MasterENG
            If eng.CheckDuplicateMasterServiceByItemCode(txt_id.Text, txt_item_code.Text) = True Then
                Config.ShowAlert("Item Code is duplicate", Me, txt_item_code.ClientID)
                ret = False
            ElseIf eng.CheckDuplicateMasterServiceByItemNameEng(txt_id.Text, txt_item_name_english.Text) = True Then
                Config.ShowAlert("Item Name English is duplicate", Me, txt_item_name_english.ClientID)
                ret = False
            ElseIf eng.CheckDuplicateMasterServiceByItemNameTH(txt_id.Text, txt_item_name_thai.Text) = True Then
                Config.ShowAlert("Item Name Thai is duplicate", Me, txt_item_name_thai.ClientID)
                ret = False
            ElseIf eng.CheckDuplicateMasterServiceByTextQueue(txt_id.Text, txt_queue.Text) = True Then
                Config.ShowAlert("Text Queue is duplicate", Me, txt_queue.ClientID)
                ret = False
            ElseIf eng.CheckDuplicateMasterServiceByItemOrder(txt_id.Text, txt_item_order.Text) = True Then
                Config.ShowAlert("Item Order is duplicate", Me, txt_item_order.ClientID)
                ret = False
            End If
            eng = Nothing
        End If

        Return ret
    End Function


    Private Sub ClearData()
        txt_id.Text = "0"
        txt_item_code.Text = ""
        txt_item_name_english.Text = ""
        txt_item_name_thai.Text = ""
        txt_standard_handing_time.Text = ""
        txt_standard_waiting_time.Text = ""
        txt_item_order.Text = ""
        txt_queue.Text = ""
        txt_colorcode.Text = ""
        txt_color.BackColor = Color.White
        chk_active.Checked = True
        chk_vasily.Checked = True
        txt_appointment_queue_no_max.Text = ""
        txt_appointment_queue_no_min.Text = ""
        txt_item_code.Enabled = True
        lblNotFound.Visible = False
        dgvService.Visible = True
        txt_search.Text = String.Empty
        SearchData()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Central Setup >> Service", "AisWebConfig.frmMSTService.aspx.CmdClear_Click", Config.GetLoginHistoryID)

    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Central Setup >> Service", "AisWebConfig.frmMSTService.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Sub SearchData()
        dgvService.CurrentPageIndex = 0

        Dim eng As New MasterENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " item_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or item_name like '%" & txt_search.Text.Trim & "%'"
            wh += " or item_name_th like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.GetServiceAllList(wh)

        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvService.Visible = False
        Else
            lblNotFound.Visible = False
            dgvService.Visible = True
            dgvService.DataSource = dt
            dgvService.DataBind()
        End If


        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub dgvService_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvService.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)

            Dim p As New CenParaDB.TABLE.TbItemCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetMasterServicePara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_item_code.Text = p.ITEM_CODE
                txt_item_name_english.Text = p.ITEM_NAME
                txt_item_name_thai.Text = p.ITEM_NAME_TH
                txt_standard_handing_time.Text = p.ITEM_TIME
                txt_standard_waiting_time.Text = p.ITEM_WAIT
                txt_item_order.Text = p.ITEM_ORDER
                txt_queue.Text = p.TXT_QUEUE

                If p.COLOR.Value <> 0 Then
                    'Dim strcol As String = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromOle(p.COLOR))
                    'If strcol.Substring(0, 1) <> "#" Then
                    '    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    'Else
                    '    txt_colorcode.Text = strcol.Replace("#", "").Trim
                    'End If
                    'txt_color.BackColor = Color.FromArgb(p.COLOR)
                    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    Dim c As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)))
                    txt_color.BackColor = c
                Else
                    txt_colorcode.Text = ""
                    txt_color.BackColor = Color.White
                End If
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                chk_vasily.Checked = IIf(p.BRAND_NAME = "Y", True, False)
                txt_appointment_queue_no_max.Text = p.APP_MAX_QUEUE
                txt_appointment_queue_no_min.Text = p.APP_MIN_QUEUE
                'txt_item_code.Enabled = True
            End If

            'SetServiceList()
        End If
    End Sub

    Protected Sub dgvService_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvService.PageIndexChanged
        dgvService.CurrentPageIndex = e.NewPageIndex
        SetServiceList()
    End Sub
End Class
