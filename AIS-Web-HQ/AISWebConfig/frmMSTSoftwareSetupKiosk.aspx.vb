Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class frmMSTSoftwareSetupKiosk
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                ClearTextBox()
                'Binddata(Request("ShopID"))
                txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))

                txtSoonestArrivalAppointment.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtLatestArrivalAppointment.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtMaxServiceAppointment.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtPreBookingPeriod.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtCancelBeforeReservationTime.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtNoPrint.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtMaximumServicePerTime.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txtRefreshWT.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Show_Video_every.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Length_of_Mobile_No.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Allowable1.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Allowable2.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Allowable3.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
                txt_Allowable4.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub ClearTextBox()
        txtSoonestArrivalAppointment.Text = ""
        txtLatestArrivalAppointment.Text = ""
        txtMaxServiceAppointment.Text = ""
        txtPreBookingPeriod.Text = ""
        txtCancelBeforeReservationTime.Text = ""
        txtMaximumServicePerTime.Text = ""
        txt_Show_Video_every.Text = ""
        txt_Length_of_Mobile_No.Text = ""
        txtNoPrint.Text = ""
        txtRefreshWT.Text = ""
        txt_Allowable1.Text = ""
        txt_Allowable2.Text = ""
        txt_Allowable3.Text = ""
        txt_Allowable4.Text = ""
        lbl_upload4.Text = ""
        lbl_header.Text = ""
        opt_now.Checked = False
        opt_schedule.Checked = True
        txt_date.Text = Now.Date.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.btn_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If ctlShopSelected1.SelectedShop.Rows.Count = 0 Then
            Config.ShowAlert("Please select Shop", Me)
            Exit Sub
        End If

        If opt_schedule.Checked Then
            If txt_date.Text <> "" Then
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) < Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Exit Sub
                End If
            End If
        End If

        If ctlBrowseFileVdo1.HasFile = True Then
            If ctlBrowseFileVdo1.TmpFileUploadPara.FileExtent.ToUpper <> ".WMV" Then
                Config.ShowAlert("Video file Extention is .WMV only", Me)
                ctlBrowseFileVdo1.ClearFile()
                Exit Sub
            End If
        ElseIf ctlBrowseFileBranner.HasFile = True Then
            If ctlBrowseFileBranner.TmpFileUploadPara.FileExtent.ToUpper <> ".JPG" And ctlBrowseFileBranner.TmpFileUploadPara.FileExtent.ToUpper <> ".PNG" Then
                Config.ShowAlert("Branner file Extention is .JPG or .PNG only", Me)
                ctlBrowseFileBranner.ClearFile()
                Exit Sub
            End If
        ElseIf ctlBrowseFileTicketHeader.HasFile = True Then
            If ctlBrowseFileTicketHeader.TmpFileUploadPara.FileExtent.ToUpper <> ".BMP" Then
                Config.ShowAlert("Ticket Header Thai file Extention is .BMP only", Me)
                ctlBrowseFileTicketHeader.ClearFile()
                Exit Sub
            End If
        ElseIf ctlBrowseFileTicketHeaderEng.HasFile = True Then
            If ctlBrowseFileTicketHeaderEng.TmpFileUploadPara.FileExtent.ToUpper <> ".BMP" Then
                Config.ShowAlert("Ticket Header Eng file Extention is .BMP only", Me)
                ctlBrowseFileTicketHeaderEng.ClearFile()
                Exit Sub
            End If
        ElseIf ctlBrowseFileKioskLogo.HasFile = True Then
            If ctlBrowseFileKioskLogo.TmpFileUploadPara.FileExtent.ToUpper <> ".PNG" Then
                Config.ShowAlert("Kiosk Logo file Extention is .PNG only", Me)
                ctlBrowseFileKioskLogo.ClearFile()
                Exit Sub
            End If
        ElseIf ctlBrowseFileKioskEloLogo.HasFile = True Then
            If ctlBrowseFileKioskEloLogo.TmpFileUploadPara.FileExtent.ToUpper <> ".JPG" Then
                Config.ShowAlert("Kiosk Elo Logo file Extention is .JPG only", Me)
                ctlBrowseFileKioskEloLogo.ClearFile()
                Exit Sub
            End If
        End If

        Dim vDateSchedule As DateTime = DateTime.Now
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim ret As Boolean = False
        If opt_now.Checked = True Then
            ret = SavedataNow()
        ElseIf opt_schedule.Checked = True Then
            DeleteCfg()
            ret = SaveDataSchedule(uPara)
            vDateSchedule = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
        End If

        If ret = True Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If ctlBrowseFileVdo1.HasFile = True Then
                Dim FileByteID As Long = SaveVDOFile(uPara.USERNAME, vDateSchedule, ctlBrowseFileVdo1, "2", trans)
                If FileByteID > 0 Then
                    ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                End If
            End If

            If ret = True Then
                If ctlBrowseFileTicketHeader.HasFile = True Then
                    Dim FileByteID As Long = SaveImageFile(uPara.USERNAME, vDateSchedule, "3", ctlBrowseFileTicketHeader, trans)
                    If FileByteID > 0 Then
                        ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                    Else
                        ret = False
                    End If
                End If

                If ret = True Then
                    If ctlBrowseFileTicketHeaderEng.HasFile = True Then
                        Dim FileByteID As Long = SaveImageFile(uPara.USERNAME, vDateSchedule, "5", ctlBrowseFileTicketHeaderEng, trans)
                        If FileByteID > 0 Then
                            ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                        Else
                            ret = False
                        End If
                    End If
                End If
                
                If ret = True Then
                    If ctlBrowseFileBranner.HasFile = True Then
                        Dim FileByteID As Long = SaveImageFile(uPara.USERNAME, vDateSchedule, "4", ctlBrowseFileBranner, trans)
                        If FileByteID > 0 Then
                            ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                        Else
                            ret = False
                        End If
                    End If
                End If

                If ret = True Then
                    If ctlBrowseFileKioskLogo.HasFile = True Then
                        Dim FileByteID As Long = SaveImageFile(uPara.USERNAME, vDateSchedule, "6", ctlBrowseFileKioskLogo, trans)
                        If FileByteID > 0 Then
                            ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                        Else
                            ret = False
                        End If
                    End If
                End If

                If ret = True Then
                    If ctlBrowseFileKioskEloLogo.HasFile = True Then
                        Dim FileByteID As Long = SaveImageFile(uPara.USERNAME, vDateSchedule, "7", ctlBrowseFileKioskEloLogo, trans)
                        If FileByteID > 0 Then
                            ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                        Else
                            ret = False
                        End If
                    End If
                End If
            End If

            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        End If
        uPara = Nothing
        If ret = True Then
            Config.ShowAlert("Save Complete", Me)
        End If
        Config.SaveTransLog("บันทึกข้อมูล : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.btn_save_Click", Config.GetLoginHistoryID)

    End Sub

    Private Function DeleteCfg() As Boolean

        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim shopid As String = dr("id")
                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    eng.DeleteCfgKioskSchedule(shopid, trans)
                    eng.DeleteCfgShopFileScheduleKiosk(shopid, trans)
                Next
            End If

            trans.CommitTransaction()
        Catch ex As Exception
            trans.RollbackTransaction()
            ret = False
        End Try
        Return ret
    End Function

    Private Function SaveShopFileSchedule(ByVal ShopFileByteID As Long, ByVal EventDate As DateTime, ByVal LoginName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False
        Dim dt As New DataTable
        dt = ctlShopSelected1.SelectedShop
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                Dim p As New CenParaDB.TABLE.TbShopFileScheduleCenParaDB
                p.SHOP_ID = dr("id")
                p.EVENT_DATE = EventDate
                p.TRANSFER_STATUS = "1"
                p.TB_SHOP_FILE_BYTE_ID = ShopFileByteID

                ret = eng.SaveShopFileSchedule(p, LoginName, trans)
                If ret = False Then
                    Config.ShowAlert(eng.ErrMessage, Me)
                    eng = Nothing
                    p = Nothing
                    Exit For
                End If
                eng = Nothing
                p = Nothing
            Next
        End If

        Return ret
    End Function

    Private Function SaveDataSchedule(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim ShopID As String = dr("id")

                    Dim p As New CenParaDB.TABLE.TbCfgSwSchedKioskCenParaDB
                    p.SHOP_ID = ShopID
                    p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
                    p.K_BEFORE = txtSoonestArrivalAppointment.Text.Trim
                    p.K_LATE = txtLatestArrivalAppointment.Text.Trim
                    p.K_MAX_APPOINTMENT = txtMaxServiceAppointment.Text.Trim
                    p.K_BEFORE_APP = txtPreBookingPeriod.Text.Trim
                    p.K_CANCEL = txtCancelBeforeReservationTime.Text.Trim
                    p.K_DISABLE = txtNoPrint.Text.Trim
                    p.K_SERVE = txtMaximumServicePerTime.Text.Trim
                    p.K_REFRESH = txtRefreshWT.Text.Trim
                    p.K_VDO = txt_Show_Video_every.Text.Trim
                    p.K_LEN = txt_Length_of_Mobile_No.Text.Trim
                    p.K_MOBILE1 = txt_Allowable1.Text.Trim
                    p.K_MOBILE2 = txt_Allowable2.Text.Trim
                    p.K_MOBILE3 = txt_Allowable3.Text.Trim
                    p.K_MOBILE4 = txt_Allowable4.Text.Trim
                    p.EVENT_STATUS = "1"

                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    ret = eng.SaveCfgSchedKiosk(p, uPara.USERNAME)
                    eng = Nothing
                Next
            End If
        End If

        Return ret
    End Function

    Private Function SavedataNow() As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim ShowError As String = ""

            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                Dim eng As New Engine.Configuration.ShopSettingENG

                For Each dr As DataRow In dt.Rows
                    Dim ShopID As String = dr("id")
                    ret = eng.SaveShopTbSetting(ShopID, txtSoonestArrivalAppointment.Text.Trim, "k_before")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtLatestArrivalAppointment.Text.Trim, "k_late")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtMaxServiceAppointment.Text.Trim, "k_max_appointment")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtPreBookingPeriod.Text.Trim, "k_before_app")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtCancelBeforeReservationTime.Text.Trim, "k_cancel")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtNoPrint.Text.Trim, "k_disable")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtMaximumServicePerTime.Text.Trim, "k_serve")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txtRefreshWT.Text.Trim, "k_refresh")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Show_Video_every.Text.Trim, "k_vdo")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Length_of_Mobile_No.Text.Trim, "k_len")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Allowable1.Text.Trim, "k_mobile1")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Allowable2.Text.Trim, "k_mobile2")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Allowable3.Text.Trim, "k_mobile3")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    ret = eng.SaveShopTbSetting(ShopID, txt_Allowable4.Text.Trim, "k_mobile4")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If

                    eng = Nothing
                Next
            End If

            If ShowError.Trim <> "" Then
                Config.ShowAlert(ShowError, Me)
                ret = False
            End If
        End If

        Return ret
    End Function

    Private Function ValidateVDOFile() As Boolean
        Dim ret As Boolean = True
        If ctlBrowseFileVdo1.HasFile = True Then
            If ctlBrowseFileVdo1.TmpFileUploadPara.FileExtent.ToUpper <> ".WMV" Then
                Config.ShowAlert("Video File is incorrect format.", Me)
                ret = False
            End If
        ElseIf ctlBrowseFileBranner.HasFile = True Then
            If ctlBrowseFileBranner.TmpFileUploadPara.FileExtent.ToUpper <> ".JPG" And ctlBrowseFileBranner.TmpFileUploadPara.FileExtent.ToUpper <> ".PNG" Then
                Config.ShowAlert("Branner is incorrect format.", Me)
                ret = False
            End If
        ElseIf ctlBrowseFileTicketHeader.HasFile = True Then
            If ctlBrowseFileTicketHeader.TmpFileUploadPara.FileExtent.ToUpper <> ".BMP" Then
                Config.ShowAlert("Ticket is incorrect format.", Me)
                ret = False
            End If
        End If
        Return ret
    End Function

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txtSoonestArrivalAppointment.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Soonest Arrival for Appointment", Me, txtSoonestArrivalAppointment.ClientID)
        ElseIf txtLatestArrivalAppointment.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Latest Arrival for Appointment ", Me, txtLatestArrivalAppointment.ClientID)
        ElseIf txtMaxServiceAppointment.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Maximum Services Appointment per time  ", Me, txtMaxServiceAppointment.ClientID)
        ElseIf txtPreBookingPeriod.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Pre-booking Period   ", Me, txtPreBookingPeriod.ClientID)
        ElseIf txtCancelBeforeReservationTime.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Cancel before Reservation Time ", Me, txtCancelBeforeReservationTime.ClientID)
        ElseIf txtNoPrint.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input No Print is W.T if more than ", Me, txtNoPrint.ClientID)
        ElseIf txtMaximumServicePerTime.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Maximum Services chosen per Time ", Me, txtMaximumServicePerTime.ClientID)
        ElseIf txtRefreshWT.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Refresh W.T every ", Me, txtRefreshWT.ClientID)
        ElseIf txt_Show_Video_every.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Show Video every ", Me, txt_Show_Video_every.ClientID)
        ElseIf txt_Length_of_Mobile_No.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Length of Mobile No ", Me, txt_Length_of_Mobile_No.ClientID)
        ElseIf opt_schedule.Checked = True Then
            If txt_date.Text.Trim = "" Then
                ret = False
                Config.ShowAlert("Please select event date ", Me)
            Else
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) <= Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    ret = False
                End If
            End If
        End If

        Return ret
    End Function

    Private Sub Binddata(ByVal ShopID As String)
        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetTbSetting(ShopID)
        If dt.Rows.Count > 0 Then
            FillData(txtSoonestArrivalAppointment, "k_before", dt)
            FillData(txtLatestArrivalAppointment, "k_late", dt)
            FillData(txtMaxServiceAppointment, "k_max_appointment", dt)
            FillData(txtPreBookingPeriod, "k_before_app", dt)
            FillData(txtCancelBeforeReservationTime, "k_cancel", dt)
            FillData(txtNoPrint, "k_disable", dt)
            FillData(txtMaximumServicePerTime, "k_serve", dt)
            FillData(txtRefreshWT, "k_refresh", dt)
            FillData(txt_Show_Video_every, "k_vdo", dt)
            FillData(txt_Length_of_Mobile_No, "k_len", dt)
            FillData(txt_Allowable1, "k_mobile1", dt)
            FillData(txt_Allowable2, "k_mobile2", dt)
            FillData(txt_Allowable3, "k_mobile3", dt)
            FillData(txt_Allowable4, "k_mobile4", dt)
        End If
        dt.Dispose()
        eng = Nothing

        Dim mEng As New Engine.Configuration.MasterENG
        ctlShopSelected1.ClearSelectedShop()
        ctlShopSelected1.SetSelectedShop(mEng.GetShopList("sh.id='" & ShopID & "'"))
        mEng = Nothing
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
    Private Function SaveImageFile(ByVal UserName As String, ByVal vDateNow As DateTime, ByVal TargetType As String, ByVal fBrowse As UserControls_ctlBrowseFile, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Long
        Dim ret As Long = 0
        Try
            Dim EventDate As String = ""
            If opt_now.Checked = True Then
                EventDate = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            ElseIf opt_schedule.Checked = True Then
                EventDate = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            End If

            Dim p As New CenParaDB.TABLE.TbShopFileByteCenParaDB
            p.TARGET_TYPE = TargetType
            p.FOLDER_NAME = System.Configuration.ConfigurationManager.AppSettings("WebConfigUploadPath") & "\" & vDateNow.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            p.FILE_NAME = DateTime.Now.ToString("yyyyMMddHHmmssfff", New Globalization.CultureInfo("en-US")) & fBrowse.TmpFileUploadPara.FileExtent
            'p.FILE_BYTE = FilePara.TmpFileByte
            p.CONVERT_STATUS = "Y"

            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            ret = eng.SaveShopFileByte(p, UserName, trans)
            If ret > 0 Then
                fBrowse.SaveFile(p.FOLDER_NAME, p.FILE_NAME)
                'fBrowse.ClearFile()
            End If
            eng = Nothing
            p = Nothing
        Catch ex As Exception
            ret = 0
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Upload File Error !!," & ex.Message & "');", True)
            Engine.Common.FunctionEng.SaveErrorLog("frmMSTSoftwareSetupKiosk.SaveImageFile", ex.Message & vbNewLine & ex.StackTrace)
        End Try

        Return ret
    End Function

    Private Function SaveVDOFile(ByVal UserName As String, ByVal vDateNow As DateTime, ByVal fBrowse As UserControls_ctlBrowseFileStream, ByVal TargetType As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Long
        Dim ret As Long = 0
        Try
            Dim EventDate As String = ""
            If opt_now.Checked = True Then
                EventDate = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            ElseIf opt_schedule.Checked = True Then
                EventDate = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            End If

            Dim p As New CenParaDB.TABLE.TbShopFileByteCenParaDB
            p.TARGET_TYPE = TargetType
            p.FOLDER_NAME = System.Configuration.ConfigurationManager.AppSettings("WebConfigUploadPath") & "\" & vDateNow.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            p.FILE_NAME = DateTime.Now.ToString("yyyyMMddHHmmssfff", New Globalization.CultureInfo("en-US")) & fBrowse.TmpFileUploadPara.FileExtent
            'p.FILE_BYTE = FilePara.TmpFileByte
            p.CONVERT_STATUS = "Y"

            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            ret = eng.SaveShopFileByte(p, UserName, trans)
            eng = Nothing
            If ret > 0 Then
                fBrowse.SaveFile(p.FOLDER_NAME, p.FILE_NAME)
                fBrowse.ClearFile()
            End If
            p = Nothing

        Catch ex As Exception
            ret = 0
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Upload File Error !!," & ex.Message & "');", True)
            Engine.Common.FunctionEng.SaveErrorLog("frmMSTSoftwareSetupKiosk.SaveVDOFile", ex.Message & vbNewLine & ex.StackTrace)
        End Try

        Return ret
    End Function


#Region "Tab VIEW"
    Private Function GetDataTabView(ByVal wh As String) As DataTable
        'Tab View
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("shop_size")
        dt.Columns.Add("shop_name")
        dt.Columns.Add("shop_code")
        dt.Columns.Add("max_service")
        dt.Columns.Add("kiosk_wt_noprint")
        dt.Columns.Add("kiosk_show_video")
        dt.Columns.Add("kiosk_langth_mobile")
        dt.Columns.Add("kiosk_retardation_video")

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            For Each shDr As DataRow In shDt.Rows
                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shDr("id"), "frmMSTSoftwareSetupKiosk_GetDataTabView")
                If shTrans.Trans IsNot Nothing Then
                    Dim MaxService As String = Engine.Common.FunctionEng.GetShopConfig("k_serve", shTrans)
                    Dim NoPrintWT As String = Engine.Common.FunctionEng.GetShopConfig("k_disable", shTrans)
                    Dim ShowVideo As String = Engine.Common.FunctionEng.GetShopConfig("k_vdo", shTrans)
                    Dim LenMobile As String = Engine.Common.FunctionEng.GetShopConfig("k_len", shTrans)
                    Dim RetardationVideo As String = Engine.Common.FunctionEng.GetShopConfig("k_retardation_vdo", shTrans)

                    Dim dr As DataRow = dt.NewRow
                    dr("id") = shDr("id")
                    dr("shop_size") = shDr("shop_size")
                    dr("shop_name") = shDr("shop_name_en")
                    dr("shop_code") = shDr("shop_code")
                    dr("max_service") = MaxService
                    dr("kiosk_wt_noprint") = NoPrintWT
                    dr("kiosk_show_video") = ShowVideo
                    dr("kiosk_langth_mobile") = LenMobile
                    dr("kiosk_retardation_video") = RetardationVideo
                    dt.Rows.Add(dr)
                End If
            Next
        Else
            lblErrorMessage.Visible = True
        End If

        If wh <> "" Then
            dt.DefaultView.RowFilter = wh
        End If
        shDt.Dispose()
        lEng = Nothing
        uPara = Nothing

        Return dt.DefaultView.ToTable
    End Function

    Private Sub BindTabView(ByVal wh As String)
        Dim dt As New DataTable
        dt = GetDataTabView(wh)
        If dt.DefaultView.Count > 0 Then
            dgvdetail.DataSource = dt.DefaultView.ToTable
            dgvdetail.DataBind()
        Else
            dgvdetail.DataSource = Nothing
            dgvdetail.DataBind()
        End If
        dt.Dispose()
    End Sub

    Protected Sub dgvdetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvdetail.ItemCreated
        If e.Item.ItemType = DataControlRowType.Header Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(uPara.USERNAME)
            If shDt.Rows.Count > 0 Then
                'Shop Size
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_size"), DropDownList), "shop_size")
                If ValSelect_ddshop_size <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_size"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = ValSelect_ddshop_size
                    End If
                End If


                'Shop Name
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_name"), DropDownList), "shop_name_en")
                If ValSelect_ddshop_name <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_name"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = ValSelect_ddshop_name
                    End If
                End If



                Dim dt As New DataTable
                dt = GetDataTabView("")
                If dt.Rows.Count > 0 Then
                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddmax_service"), DropDownList), "max_service")
                    If ValSelect_ddmax_service <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddmax_service"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = ValSelect_ddmax_service
                        End If
                    End If


                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddkiosk_wt_noprint"), DropDownList), "kiosk_wt_noprint")
                    If ValSelect_ddkiosk_wt_noprint <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddkiosk_wt_noprint"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = ValSelect_ddkiosk_wt_noprint
                        End If
                    End If


                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddkiosk_show_video"), DropDownList), "kiosk_show_video")
                    If ValSelect_ddkiosk_show_video <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddkiosk_show_video"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = ValSelect_ddkiosk_show_video
                        End If
                    End If


                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddkiosk_langth_mobile"), DropDownList), "kiosk_langth_mobile")
                    If ValSelect_ddkiosk_langth_mobile <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddkiosk_langth_mobile"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = ValSelect_ddkiosk_langth_mobile
                        End If
                    End If


                    'BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddkiosk_retardation_video"), DropDownList), "kiosk_retardation_video")
                    'If ValSelect_ddkiosk_retardation_video <> "" Then
                    '    Dim ddl As New DropDownList
                    '    ddl = DirectCast(e.Item.FindControl("ddkiosk_retardation_video"), DropDownList)
                    '    If Not ddl Is Nothing Then
                    '        ddl.SelectedValue = ValSelect_ddkiosk_retardation_video
                    '    End If
                    'End If

                End If
                dt.Dispose()
            End If
        End If
    End Sub

    Private Sub BindDataToDropdownlist(ByVal dt As DataTable, ByVal ddl As DropDownList, ByVal DataTxtFld As String)
        Dim dtDDl As New DataTable
        dtDDl = dt.DefaultView.ToTable(True, DataTxtFld)
        dtDDl.DefaultView.Sort = DataTxtFld
        If dtDDl.Rows.Count > 0 Then
            Dim dr As DataRow = dtDDl.NewRow
            dr(DataTxtFld) = "All"
            dtDDl.Rows.InsertAt(dr, 0)
            ddl.DataTextField = DataTxtFld
            ddl.DataSource = dtDDl
            ddl.DataBind()
        End If
        dtDDl.Dispose()
    End Sub

    Dim ValSelect_ddshop_size As String = ""
    Dim ValSelect_ddshop_name As String = ""
    Dim ValSelect_ddmax_service As String = ""
    Dim ValSelect_ddkiosk_wt_noprint As String = ""
    Dim ValSelect_ddkiosk_show_video As String = ""
    Dim ValSelect_ddkiosk_langth_mobile As String = ""
    Dim ValSelect_ddkiosk_retardation_video As String = ""

    Protected Sub ddshop_size_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddshop_size = dd.SelectedValue.ToString
                BindTabView("shop_size = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddshop_size = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Size: Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddshop_size_SelectedIndexChanged", Config.GetLoginHistoryID)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddshop_name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddshop_name = dd.SelectedValue.ToString
                BindTabView("shop_name = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddshop_name = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Name: Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddshop_name_SelectedIndexChanged", Config.GetLoginHistoryID)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddmax_service_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddmax_service = dd.SelectedValue.ToString
                BindTabView("max_service = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddmax_service = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Maximum Service Chosen Per Time(Service): Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddmax_service_SelectedIndexChanged", Config.GetLoginHistoryID)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddkiosk_wt_noprint_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddkiosk_wt_noprint = dd.SelectedValue.ToString
                BindTabView("kiosk_wt_noprint = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddkiosk_wt_noprint = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม No Print is W.T more than(Minute): Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddkiosk_wt_noprint_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddkiosk_show_video_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddkiosk_show_video = dd.SelectedValue.ToString
                BindTabView("kiosk_show_video = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddkiosk_show_video = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Show video Every(Minute): Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddkiosk_show_video_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddkiosk_langth_mobile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddkiosk_langth_mobile = dd.SelectedValue.ToString
                BindTabView("kiosk_langth_mobile = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddkiosk_langth_mobile = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Length of Mobile no(Digit): Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddkiosk_langth_mobile_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddkiosk_retardation_video_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                ValSelect_ddkiosk_retardation_video = dd.SelectedValue.ToString
                BindTabView("kiosk_retardation_video = '" & dd.SelectedItem.Text & "'")
            Else
                ValSelect_ddkiosk_retardation_video = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม ความหน่วงของไฟล์ video(second): Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.ddkiosk_retardation_video_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub lbl_shop_Name_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            TabContainer1.ActiveTabIndex = "0"
            Dim lbl_shop_Name As LinkButton = DirectCast(sender, LinkButton)
            Dim gvr As DataGridItem = DirectCast(lbl_shop_Name.NamingContainer, DataGridItem)
            Dim lblID As Label = gvr.FindControl("lbl_id")
            Binddata(lblID.Text)

            Config.SaveTransLog("เลือกข้อมูล - " & lbl_shop_Name.Text & ": Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.lbl_shop_Name_Click", Config.GetLoginHistoryID)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        If TabContainer1.ActiveTabIndex = 1 Then
            BindTabView("1=1")
        End If
    End Sub
#End Region
    

End Class
