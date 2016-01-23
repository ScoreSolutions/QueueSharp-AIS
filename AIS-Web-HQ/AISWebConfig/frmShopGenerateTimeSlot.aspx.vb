Imports System
Imports System.Data
Imports System.Data.OleDb
Imports Engine.Common
Imports Engine.Configuration
Imports ShParaDB.TABLE

Partial Class frmShopGenerateTimeSlot
    Inherits System.Web.UI.Page
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            SetShopTime()
            SetItemList()
            SetReCurenceDay()

            txt_No_of_counter.Text = "1"
            txt_Slot_Time.Text = "30"
            uPara = Nothing

            txt_No_of_counter.Attributes.Add("onKeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")

            'lblShopOperationH.Text = _s_open & " - " & _s_close
            txtDateFrom.Text = Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
            txtDateTo.Text = GetLastDateOfMonth()
            lblDateRange.Text = txtDateFrom.Text & " - " & txtDateTo.Text
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Appointment >> Appointment Slot ", "AisWebConfig.frmShopGenerateTimeSlot.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub


    Sub SetTimeSlot()
        chkListTimeSlot.Items.Clear()
        Dim arrList As New ArrayList
        arrList = GetTimeSlot()
        For i As Integer = 0 To arrList.Count - 1
            chkListTimeSlot.Items.Add(arrList(i))
        Next
    End Sub

    Function GetLastDateOfMonth() As String
        Dim LastDay As DateTime = Now.AddMonths(1)
        LastDay = LastDay.AddDays(-(Now.Day))
        Dim strLastDay As String = LastDay.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        Return strLastDay
    End Function

    Function GetTimeSlot() As ArrayList
        Dim arrList As New ArrayList
        Dim StartTime As Date = CDate(ddlStartTime.SelectedValue)
        Dim EndTime As Date = CDate(ddlEndTime.SelectedValue)
        Dim SlotTime As Date = StartTime
        Dim slot As Integer = txt_Slot_Time.Text
        'Dim ret As Boolean = False
        Do
            If SlotTime < EndTime Then
                arrList.Add(SlotTime.Hour.ToString.PadLeft(2, "0") & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"))
            ElseIf SlotTime = EndTime Or (SlotTime > EndTime And SlotTime <> EndTime) Then
                arrList.Add(EndTime.Hour.ToString.PadLeft(2, "0") & ":" & EndTime.Minute.ToString.PadLeft(2, "0"))
                Exit Do
            Else
                Exit Do
            End If
            SlotTime = DateAdd(DateInterval.Minute, slot, SlotTime)
        Loop While SlotTime <= EndTime

        Return arrList
    End Function

    Sub SetReCurenceDay()
        chkRecurrenceDay.Items.Add("Monday")
        chkRecurrenceDay.Items.Add("Tuesday")
        chkRecurrenceDay.Items.Add("Wednesday")
        chkRecurrenceDay.Items.Add("Thursday")
        chkRecurrenceDay.Items.Add("Friday")
        chkRecurrenceDay.Items.Add("Saturday")
        chkRecurrenceDay.Items.Add("Sunday")
    End Sub

    Private Sub SetShopTime()
        Dim StartTime As New Date(1, 1, 1, 0, 0, 0)
        Dim EndTime As New Date(1, 1, 1, 23, 30, 0)
        Dim CurrTime As Date = StartTime
        Do
            ddlStartTime.Items.Add(New ListItem(CurrTime.ToString("HH:mm"), CurrTime.ToString("HH:mm")))
            ddlEndTime.Items.Add(New ListItem(CurrTime.ToString("HH:mm"), CurrTime.ToString("HH:mm")))
            CurrTime = DateAdd(DateInterval.Minute, 30, CurrTime)
        Loop While CurrTime <= EndTime
        ddlStartTime.SelectedIndex = 12
        ddlEndTime.SelectedIndex = 12
    End Sub

    Private Sub SetItemList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetServiceByAppointMent()
        If dt.Rows.Count > 0 Then
            gvService.DataSource = dt
            gvService.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdGenSlot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGenSlot.Click
        If ValidateSlot() = True Then
            SetTimeSlot()
        End If

    End Sub

    Private Function ValidateSlot() As Boolean
        If txt_No_of_counter.Text.Trim = "0" Then
            Config.ShowAlert("No. Counter is not less than 1", Me, txt_No_of_counter.ClientID)
            Return False
        End If
        If ddlStartTime.SelectedValue >= ddlEndTime.SelectedValue Then
            Config.ShowAlert("Start Time not over than End Time", Me, txt_No_of_counter.ClientID)
            Return False
        End If

        Return True
    End Function

    Private Function Validation() As Boolean
        'Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            Config.ShowAlert("Please select Shop", Me)
            Return False
        End If

        If txtDateFrom.Text = "" Then
            Config.ShowAlert("Please select Date From", Me)
            Return False
        End If
        If txtDateTo.Text = "" Then
            Config.ShowAlert("Please select Date To", Me)
            Return False
        End If

        Dim vDateFrom As Date = FunctionEng.cStrToDate2(txtDateFrom.Text)
        Dim vDateTo As Date = FunctionEng.cStrToDate2(txtDateTo.Text)

        If vDateFrom <= DateTime.Now.Date Then
            Config.ShowAlert("Your Date Range is incorrect. Please reselect the Date Range", Me)
            Return False
        End If

        If vDateFrom.DayOfWeek <> DayOfWeek.Monday Then
            Config.ShowAlert("Date Range is Start on Monday", Me)
            Return False
        End If
        If vDateTo.DayOfWeek <> DayOfWeek.Sunday Then
            Config.ShowAlert("Date Range is Finish on Sunday", Me)
            Return False
        End If
        If vDateFrom > vDateTo Then
            Config.ShowAlert("Your Date Range is incorrect. Please reselect the Date Range", Me)
            Return False
        End If

        If ValidateSlot() = False Then
            Return False
        End If

        Dim IsSelect As Boolean = False
        For i As Integer = 0 To chkListTimeSlot.Items.Count - 1
            If chkListTimeSlot.Items(i).Selected Then
                IsSelect = True
                Exit For
            End If
        Next
        If IsSelect = False Then
            Config.ShowAlert("Please select time", Me)
            Return False
        End If

        Dim IsSelectService As Boolean = False
        For Each grv As DataGridItem In gvService.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked Then
                IsSelectService = True
                Exit For
            End If
        Next

        If IsSelectService = False Then
            Config.ShowAlert("Please select service", Me)
            Return False
        End If

        'ถ้ามีลูกค้าทำรายการจองอยู่แล้วในช่วงเวลาที่เลือก
        Dim ret As Boolean = True
        Dim _err As String = ""
        For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
            Dim eng As New Engine.Configuration.ShopAppointmentTimeSlotENG
            If eng.CheckCustomerAppointment(vDateFrom, vDateTo, c.Value) = True Then
                ret = False
                _err = eng.ErrorMessage
            End If
            eng = Nothing

            If ret = False Then
                Exit For
            End If
        Next

        If ret = False Then
            Config.ShowAlert(_err, Me)
            Return False
        End If
        
        Return True
    End Function

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            '==1 หา Time Slot ที่จะบันทึก
            Dim arrListSlot As New ArrayList
            Dim IsContinue As Boolean = False
            Dim StartTime As String = ""
            For i As Integer = 0 To chkListTimeSlot.Items.Count - 1
                Dim arr() As String = {"", ""}
                If chkListTimeSlot.Items(i).Selected Then
                    Dim tmpSTime As String = chkListTimeSlot.Items(i).Text
                    Dim tmpETime As String = chkListTimeSlot.Items(i).Text
                    If IsContinue = False Then
                        StartTime = tmpSTime
                    End If

                    Dim _StartTime As Date = CDate(StartTime)
                    Dim _EndTime As Date = CDate(tmpETime)

                    arr(0) = _StartTime.ToString("HH:mm")
                    arr(1) = _EndTime.ToString("HH:mm")
                    'arr(1) = DateAdd(DateInterval.Minute, 30, _EndTime)
                    If IsContinue Then
                        arrListSlot.RemoveAt(arrListSlot.Count - 1)
                    End If
                    arrListSlot.Add(arr)
                    IsContinue = True
                Else
                    IsContinue = False
                End If
            Next
            
            Dim arrListDay As New ArrayList
            For i As Integer = 0 To chkRecurrenceDay.Items.Count - 1
                If chkRecurrenceDay.Items(i).Selected Then
                    arrListDay.Add(chkRecurrenceDay.Items(i).Text)
                End If
            Next
            If arrListDay.Count = 0 Then
                For i As Integer = 0 To chkRecurrenceDay.Items.Count - 1
                    arrListDay.Add(chkRecurrenceDay.Items(i).Text)
                Next
            End If

           

            
            For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                Dim DateFrom As Date = FunctionEng.cStrToDate2(txtDateFrom.Text)
                Dim DateTo As Date = FunctionEng.cStrToDate2(txtDateTo.Text)

                Dim strDateFrom As String = DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim strDateTo As String = DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                '== ลบข้อมูลเดิม
                Dim eng As New Engine.Configuration.ShopSettingENG
                eng.DeleteAppointmentSchedule(strDateFrom, strDateFrom, c.Value)
                If eng.ErrorMessage <> "" Then
                    Exit For
                End If

                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(c.Value, "frmShopGenerateTimeSlot_CmdSave.Click")
                Dim _s_open As String = FunctionEng.GetShopConfig("s_open", shTrans)
                Dim _s_close As String = FunctionEng.GetShopConfig("s_close", shTrans)
                Dim _k_before_close As String = txtBeforeShopClose.Text

                '==2 วนลูปวันที่  2.1 วนลูป  slot
                Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
                Try
                    While DateFrom <= DateTo
                        Dim CurrDate As String = DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                        Dim day As String = DateFrom.DayOfWeek.ToString
                        Dim IsSave As Boolean = False
                        For i As Integer = 0 To arrListDay.Count - 1
                            If day = arrListDay(i) Then IsSave = True : Exit For
                        Next

                        If IsSave Then

                            For i As Integer = 0 To arrListSlot.Count - 1
                                Dim _arr() As String = arrListSlot(i)
                                Dim p As New ShParaDB.TABLE.TbAppointmentScheduleShParaDB
                                With p
                                    .APP_DATE = DateFrom
                                    .CAPACITY = txt_No_of_counter.Text
                                    '.GAP = 2

                                    '_arr(0) = เวลาที่เลือกตรง StartTime
                                    If _arr(0) < _s_open Then
                                        .START_TIME = DateFrom & " " & _s_open
                                    Else
                                        .START_TIME = DateFrom & " " & _arr(0)
                                    End If

                                    '_arr(1) = เวลาที่เลือกตรง EndTime
                                    If _arr(1) > _s_close Then
                                        .END_TIME = DateFrom & " " & _s_close
                                    Else
                                        .END_TIME = DateFrom & " " & _arr(1)
                                    End If
                                    .START_SLOT = DateFrom & " " & _s_open
                                    .END_SLOT = DateFrom & " " & _s_close
                                    .SLOT = txt_Slot_Time.Text
                                End With

                                eng.SaveAppointmentSchedule(p, uPara.USER_PARA.ID, shTrans)

                            Next

                            'Dim p As New ShParaDB.TABLE.TbAppointmentScheduleShParaDB
                            'With p
                            '    .APP_DATE = DateFrom
                            '    .CAPACITY = txt_No_of_counter.Text
                            '    .GAP = 2
                            '    .START_TIME = DateFrom & " " & _s_open
                            '    .END_TIME = DateFrom & " " & _s_close
                            '    .START_SLOT = DateFrom & " " & _s_open
                            '    .END_SLOT = DateFrom & " " & _s_close
                            '    .SLOT = txt_Slot_Time.Text
                            'End With


                            If eng.DeleteAppointmentService(CurrDate, shTrans) = True Then
                                For Each grv As DataGridItem In gvService.Items
                                    Dim chk As CheckBox = grv.FindControl("chk")
                                    If chk.Checked = True Then
                                        Dim lbl_id As Label = grv.FindControl("lbl_id")
                                        Dim shIlnq As New ShLinqDB.TABLE.TbItemShLinqDB
                                        shIlnq.ChkDataByWhere("master_itemid = '" & lbl_id.Text & "'", shTrans.Trans)
                                        If shIlnq.ID <> 0 Then
                                            Dim pi As New ShParaDb.TABLE.TbAppointmentItemShParaDB
                                            pi.APP_DATE = DateFrom
                                            pi.ITEM_ID = shIlnq.ID

                                            If eng.SaveAppointmentService(pi, uPara.USER_PARA.ID, shTrans) = False Then
                                                shTrans.RollbackTransaction()
                                                Config.ShowAlert(eng.ErrorMessage, Me)
                                                shIlnq = Nothing
                                                Exit Sub
                                            End If
                                        End If
                                        shIlnq = Nothing
                                    End If
                                Next
                            Else
                                shTrans.RollbackTransaction()
                                Config.ShowAlert(eng.ErrorMessage, Me)
                                Exit Sub
                            End If


                            If eng.DeleteAppointmentSlot(CurrDate, shTrans) = True Then
                                Dim ShopCloseTime As DateTime = DateAdd(DateInterval.Minute, (CInt(_k_before_close) + CInt(txt_Slot_Time.Text)) * -1, CDate(_s_close))
                                For Each chk As ListItem In chkListTimeSlot.Items
                                    If chk.Value <= ShopCloseTime.ToString("HH:mm") Then
                                        If chk.Selected = True Then

                                            If chk.Value >= _s_open And chk.Value <= _s_close Then
                                                Dim pl As New ShParaDB.TABLE.TbAppointmentSlotShParaDB
                                                pl.APP_DATE = DateFrom
                                                pl.CAPACITY = txt_No_of_counter.Text
                                                pl.SLOT_TIME = DateFrom & " " & chk.Value
                                                pl.IN_USE = 0

                                                If eng.SaveAppointmentSlot(pl, uPara.USERNAME, shTrans) = False Then
                                                    shTrans.RollbackTransaction()
                                                    Config.ShowAlert(eng.ErrorMessage, Me)
                                                    Exit Sub
                                                End If
                                            End If

                                        End If
                                    End If
                                Next
                            Else
                                shTrans.RollbackTransaction()
                                Config.ShowAlert(eng.ErrorMessage, Me)
                                Exit Sub
                            End If
                        End If

                        If eng.ErrorMessage <> "" Then
                            shTrans.RollbackTransaction()
                            Exit Sub
                        End If
                        ' End If

                        DateFrom = DateAdd(DateInterval.Day, 1, DateFrom)
                    End While

                    If eng.ErrorMessage = "" Then
                        shTrans.CommitTransaction()
                        Dim en As New Engine.Configuration.ShopSettingENG
                        en.SaveShopTbSetting(c.Value, _k_before_close, "k_before_close")
                        en = Nothing
                    End If
                Catch ex As Exception
                    shTrans.RollbackTransaction()
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert(" & ex.Message.ToString & " );", True)
                    Exit Sub
                End Try
            Next
            Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Appointment >> Appointment Slot ", "AisWebConfig.frmShopGenerateTimeSlot.aspx.CmdSave_Click", Config.GetLoginHistoryID)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Save Complete');", True)
        End If
    End Sub

    Protected Sub btnTemp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemp.Click
        lblDateRange.Text = txtDateFrom.Text & IIf(txtDateTo.Text <> "", " - ", "") & txtDateTo.Text
    End Sub

    Protected Sub chkRecurrenceDay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRecurrenceDay.SelectedIndexChanged
        Dim strDay As String = ""
        For i As Integer = 0 To chkRecurrenceDay.Items.Count - 1
            If chkRecurrenceDay.Items(i).Selected = True Then
                strDay &= chkRecurrenceDay.Items(i).Text & ", "
            End If
        Next
        If strDay <> "" Then
            lblRecuringOn.Text = strDay.Substring(0, strDay.Length - 2)
        Else
            lblRecuringOn.Text = ""
        End If
    End Sub

    Sub GetOperationHour(ByVal arrBranch As ArrayList)
        If arrBranch.Count = 0 Then
            Exit Sub
        End If

        lblShopOperationH.Text = ""
        For cnt As Integer = 0 To arrBranch.Count - 1

            Dim _s_open As String = ""
            Dim _s_close As String = ""
            Dim ShopID As String = ""
            Dim ShopName As String = ""
            Dim arr() As String = arrBranch(cnt).ToString.Split("|")
            If arr.Length > 0 Then
                ShopID = arr(0)
                ShopName = arr(1)
                Dim eng As New Engine.Configuration.ShopSettingENG
                Dim dt As New DataTable
                dt = eng.GetTbSettingOpenAndCloseTime(ShopID)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim temp() As DataRow
                        temp = dt.Select("config_name = 's_open'")
                        If temp.Length > 0 Then
                            _s_open = temp(0).Item("config_value")
                        End If

                        Dim temp2() As DataRow
                        temp2 = dt.Select("config_name = 's_close'")
                        If temp2.Length > 0 Then
                            _s_close = temp2(0).Item("config_value")
                        End If
                    Next
                End If
            End If
 
            lblShopOperationH.Text &= ShopName & " : " & _s_open & " - " & _s_close & "<br/>"
        Next
    End Sub

    Sub clear()
        For Each grv As DataGridItem In gvService.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            chk.Checked = False
        Next

        For i As Integer = 0 To chkRecurrenceDay.Items.Count - 1
            chkRecurrenceDay.Items(i).Selected = False
        Next

        txtDateFrom.Text = Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        txtDateTo.Text = GetLastDateOfMonth()
        lblDateRange.Text = txtDateFrom.Text & " - " & txtDateTo.Text
        'lblShopOperationH.Text = ""
        lblRecuringOn.Text = ""
        ddlStartTime.SelectedIndex = 0
        ddlEndTime.SelectedIndex = 0
        chkListTimeSlot.Items.Clear()

        txt_No_of_counter.Text = "1"
        txt_Slot_Time.Text = "30"
    End Sub

    Protected Sub ctlBranchSelected1_TreeViewBranchChange(ByVal arrBranch As ArrayList) Handles ctlBranchSelected1.TreeViewBranchChange
        GetOperationHour(arrBranch)
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        clear()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Appointment Slot ", "AisWebConfig.frmShopGenerateTimeSlot.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub
End Class
