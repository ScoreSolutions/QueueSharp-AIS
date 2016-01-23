Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class frmMSTWebAppSetupAppointment
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                ClearTextBox()
                Binddata()

                txtMinAppointmentHour.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtMaxAppointmentDay.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtMaxEditAppointmentHour.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")

                txtAppointmentNoShowQty.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtAppointmentWithinDay.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtNoBookDay.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Web Applicaiton Setup >> Appointment", "AisWebConfig.frmMSTWebAppSetupAppointment.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub ClearTextBox()
        txtMinAppointmentHour.Text = ""
        txtMaxAppointmentDay.Text = ""
        txtMaxEditAppointmentHour.Text = ""
        txtAppointmentNoShowQty.Text = ""
        txtAppointmentWithinDay.Text = ""
        txtNoBookDay.Text = ""
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Web Applicaiton Setup >> Appointment", "AisWebConfig.frmMSTWebAppSetupAppointment.aspx.btn_clear_Click", Config.GetLoginHistoryID)

    End Sub

    

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txtMinAppointmentHour.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Minimum Appointment Hour", Me, txtMinAppointmentHour.ClientID)
        ElseIf txtMaxAppointmentDay.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Maximum Appointment Day", Me, txtMaxAppointmentDay.ClientID)
        ElseIf txtMaxEditAppointmentHour.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Maximum Edit Appointment Hour", Me, txtMaxEditAppointmentHour.ClientID)
        ElseIf txtMaxAppointmentService.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Maximum Appointment Service", Me, txtMaxAppointmentService.ClientID)
        ElseIf txtAppointmentNoShowQty.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input No show Appointment Times", Me, txtAppointmentNoShowQty.ClientID)
        ElseIf txtAppointmentWithinDay.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input No show within Day", Me, txtAppointmentWithinDay.ClientID)
        ElseIf txtNoBookDay.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input customer cannot pre-book for the next Day", Me, txtAppointmentWithinDay.ClientID)
        ElseIf Convert.ToInt16(txtAppointmentWithinDay.Text) >= Convert.ToInt16(txtNoBookDay.Text) Then
            ret = False
            Config.ShowAlert("Please input No show within Day less than pre-book for the next Day", Me, txtAppointmentWithinDay.ClientID)
        ElseIf ddlSmsTimeFrom.SelectedValue >= ddlSmsTimeTo.SelectedValue Then
            ret = False
            Config.ShowAlert("SMS and Email Alert Time From is over Time To", Me)
        End If

        Return ret
    End Function

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Dim vDateSchedule As DateTime = DateTime.Now
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim ret As Boolean = False
        Dim ShowError As String = ""
        If Validation() = True Then
            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            ret = eng.SaveSysconfig(txtMinAppointmentHour.Text.Trim, "MinAppointmentHour", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtMaxAppointmentDay.Text.Trim, "MaxAppointmentDay", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtMaxEditAppointmentHour.Text.Trim, "MaxEditAppointmentHour", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtMaxAppointmentService.Text.Trim, "MaxAppointmentService", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtAppointmentNoShowQty.Text.Trim, "AppointmentNoShowQty", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtAppointmentWithinDay.Text.Trim, "AppointmentWithinDay", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            ret = eng.SaveSysconfig(txtNoBookDay.Text.Trim, "NoBookDay", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            Dim SmsTime As String = ddlSmsTimeFrom.SelectedValue & "-" & ddlSmsTimeTo.SelectedValue
            ret = eng.SaveSysconfig(SmsTime, "AppointmentSMSNotifyTime", uPara.USERNAME)
            If ret = False Then
                ShowError += eng.ErrMessage
            End If

            eng = Nothing
        End If

        uPara = Nothing
        Config.SaveTransLog("บันทึกข้อมูล : Web Applicaiton Setup >> Appointment", "AisWebConfig.frmMSTWebAppSetupAppointment.aspx.btn_save_Click", Config.GetLoginHistoryID)

        If ShowError.Trim = "" Then
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert(ShowError, Me)
        End If
    End Sub

    Private Sub Binddata()
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim dt As New DataTable
        dt = eng.GetSysconfigDT
        If dt.Rows.Count > 0 Then
            FillData(txtMinAppointmentHour, "MinAppointmentHour", dt)
            FillData(txtMaxAppointmentDay, "MaxAppointmentDay", dt)
            FillData(txtMaxEditAppointmentHour, "MaxEditAppointmentHour", dt)
            FillData(txtMaxAppointmentService, "MaxAppointmentService", dt)
            FillData(txtAppointmentNoShowQty, "AppointmentNoShowQty", dt)
            FillData(txtAppointmentWithinDay, "AppointmentWithinDay", dt)
            FillData(txtNoBookDay, "NoBookDay", dt)

            Dim SmsTimeLen As String = Engine.Common.FunctionEng.GetQisDBConfig("AppointmentSMSNotifyTime")
            Dim SmsTime() As String = Split(SmsTimeLen, "-")
            If SmsTime.Length = 2 Then
                Dim tFrom() As String = Split(SmsTime(0), ":")
                If tFrom.Length = 2 Then
                    If tFrom(1) = "00" Then
                        ddlSmsTimeFrom.SelectedValue = SmsTime(0)
                    Else
                        ddlSmsTimeFrom.SelectedValue = "08:00"
                    End If
                Else
                    ddlSmsTimeFrom.SelectedValue = "08:00"
                End If

                Dim tTo() As String = Split(SmsTime(1), ":")
                If tTo.Length = 2 Then
                    If tTo(1) = "00" Then
                        ddlSmsTimeTo.SelectedValue = SmsTime(1)
                    Else
                        ddlSmsTimeTo.SelectedValue = "17:00"
                    End If
                Else
                    ddlSmsTimeTo.SelectedValue = "17:00"
                End If
            End If
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub FillData(ByVal txtBox As TextBox, ByVal ConfigName As String, ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "config_name = '" & ConfigName & "'"
            If dt.DefaultView.Count > 0 Then
                txtBox.Text = dt.DefaultView(0)("config_value")
            Else
                Config.ShowAlert("Config value not found : " & ConfigName, Me)
            End If
        End If
    End Sub
End Class
