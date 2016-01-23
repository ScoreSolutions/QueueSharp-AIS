Imports ShLinqDB
Imports ShParaDB
Imports System.Data

Partial Class frmShopAutoForce
    Inherits System.Web.UI.Page
    Dim _s_open As String
    Dim _s_close As String
    Dim _ShopID As String

    WriteOnly Property open() As String
        Set(ByVal value As String)
            _s_open = value
        End Set
    End Property

    WriteOnly Property close() As String
        Set(ByVal value As String)
            _s_close = value
        End Set
    End Property

    WriteOnly Property ShopID() As String
        Set(ByVal value As String)
            _ShopID = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
        Dim shEng As New Engine.Configuration.MasterENG
        sh = shEng.GetShopPara(Request("ShopID"))
        If sh.ID <> 0 Then
            lblScreenName.Text = "Shop Setup >> Auto Force >> " & sh.SHOP_NAME_EN
        End If
        sh = Nothing
        shEng = Nothing

        If Request("ShopID") Is Nothing Then
            Response.Redirect("frmShopSelectedShop.aspx?MenuID=" & Request("MenuID"))
        Else
            ShopID = Request.QueryString("ShopID")
            GetOperationHour()
        End If


        If Not IsPostBack Then
            lblShopOperationH.Text = _s_open & " - " & _s_close
            SetReCurenceDay()
            SetTimeSlot()
            Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopAutoForce.aspx.Page_Load", Config.GetLoginHistoryID)

            txtDateFrom.Text = Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
            txtDateTo.Text = GetLastDateOfMonth()
            lblDateRange.Text = txtDateFrom.Text & " - " & txtDateTo.Text
        End If

    End Sub

    Sub SetReCurenceDay()
        chkRecurrenceDay.Items.Add("Monday")
        chkRecurrenceDay.Items.Add("Tuesday")
        chkRecurrenceDay.Items.Add("Wednesday")
        chkRecurrenceDay.Items.Add("Thursday")
        chkRecurrenceDay.Items.Add("Friday")
        chkRecurrenceDay.Items.Add("Saturday")
        chkRecurrenceDay.Items.Add("Sunday")
    End Sub

    Sub SetTimeSlot()
        Dim arrList As New ArrayList
        arrList = GetTimeSlot()
        For i As Integer = 0 To arrList.Count - 1
            chkListTimeSlot.Items.Add(arrList(i))
        Next
    End Sub

    Function GetFirstDateOfMonth() As String
        Dim FirstDay As DateTime = Now.AddDays(-(Now.Day - 1))
        Dim strFirstDay As String = FirstDay.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        Return strFirstDay
    End Function

    Function GetLastDateOfMonth() As String
        Dim LastDay As DateTime = Now.AddMonths(1)
        LastDay = LastDay.AddDays(-(Now.Day))
        Dim strLastDay As String = LastDay.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        Return strLastDay
    End Function

    Function GetTimeSlot() As ArrayList
        Dim arrList As New ArrayList
        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetTbSettingOpenAndCloseTime(_ShopID)
        Dim s_open As String = ""
        Dim s_close As String = ""
        If dt.Rows.Count > 1 Then
            Dim temp() As DataRow
            temp = dt.Select("config_name = 's_open'")
            If temp.Length > 0 Then
                s_open = temp(0).Item("config_value")
            End If

            Dim temp2() As DataRow
            temp2 = dt.Select("config_name = 's_close'")
            If temp2.Length > 0 Then
                s_close = temp2(0).Item("config_value")
            End If
        End If

        If s_open = "" Or s_close = "" Then
            Return arrList
        End If


        Dim StartTime As Date = CDate(s_open)
        Dim EndTime As Date = CDate(s_close)
        Dim SlotTime As Date = StartTime
        Dim slot As Integer = 30
        Dim ret As Boolean = False
        For i As Int32 = 0 To 1000
            If SlotTime < EndTime Then
                arrList.Add(SlotTime.Hour.ToString.PadLeft(2, "0") & ":" & SlotTime.Minute.ToString.PadLeft(2, "0"))
            ElseIf SlotTime = EndTime Or (SlotTime > EndTime And SlotTime <> EndTime) Then
                arrList.Add(EndTime.Hour.ToString.PadLeft(2, "0") & ":" & EndTime.Minute.ToString.PadLeft(2, "0"))
                Exit For
            Else
                Exit For
            End If
            SlotTime = DateAdd(DateInterval.Minute, slot, SlotTime)
        Next
        Return arrList
    End Function

    Sub GetOperationHour()

        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetTbSettingOpenAndCloseTime(_ShopID)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                'strTime &= dt.Rows(i).Item("config_value").ToString
                'If i <> dt.Rows.Count - 1 Then
                '    strTime &= " - "
                'End If

                Dim temp() As DataRow
                temp = dt.Select("config_name = 's_open'")
                If temp.Length > 0 Then
                    open = temp(0).Item("config_value")
                End If

                Dim temp2() As DataRow
                temp2 = dt.Select("config_name = 's_close'")
                If temp2.Length > 0 Then
                    close = temp2(0).Item("config_value")
                End If
            Next
        End If

    End Sub

    Protected Sub btnTemp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemp.Click
        lblDateRange.Text = txtDateFrom.Text & IIf(txtDateTo.Text <> "", " - ", "") & txtDateTo.Text
    End Sub

    Protected Sub chkRecurrenceDay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRecurrenceDay.SelectedIndexChanged
        Dim strDay As String = ""
        Dim cnt As Integer = 0
        For i As Integer = 0 To chkRecurrenceDay.Items.Count - 1
            If chkRecurrenceDay.Items(i).Selected = True Then
                cnt += 1
                strDay &= chkRecurrenceDay.Items(i).Text & ","
                If cnt = 4 Then strDay &= "<br/>"
            End If
        Next
        If strDay <> "" Then
            lblRecuringOn.Text = strDay.Substring(0, strDay.Length - 1)
        Else
            lblRecuringOn.Text = ""
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then

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

                    arr(0) = _StartTime
                    arr(1) = _EndTime
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

            '== ลบข้อมูลเดิม
            Dim strDateFrom() As String = txtDateFrom.Text.Split("/")
            Dim strDateEnd() As String = txtDateTo.Text.Split("/")
            Dim eng As New Engine.Configuration.ShopSettingENG
            If eng.DeleteAutoForce(strDateFrom(2) & strDateFrom(1) & strDateFrom(0), _
                                   strDateEnd(2) & strDateEnd(1) & strDateEnd(0), _ShopID) = False Then
                Config.ShowAlert(eng.ErrorMessage, Me)
                Exit Sub
            End If

            '==2 วนลูปวันที่  2.1 วนลูป  slot
            Try
                Dim DateFrom As New DateTime(strDateFrom(2), strDateFrom(1), strDateFrom(0))
                Dim DateTo As New DateTime(strDateEnd(2), strDateEnd(1), strDateEnd(0))
                While DateFrom <= DateTo
                    Dim day As String = DateFrom.DayOfWeek.ToString
                    Dim IsSave As Boolean = False
                    For i As Integer = 0 To arrListDay.Count - 1
                        If day = arrListDay(i) Then IsSave = True : Exit For
                    Next

                    If IsSave Then

                        For i As Integer = 0 To arrListSlot.Count - 1
                            Dim _arr() As String = arrListSlot(i)
                            Dim p As New ShParaDB.TABLE.TbForceScheduleShParaDB
                            With p
                                .FORCE_DATE = DateFrom
                                .START_TIME = DateFrom & " " & _arr(0)
                                .END_TIME = DateFrom & " " & _arr(1)
                                .START_SLOT = DateFrom & " " & _s_open
                                .END_SLOT = DateFrom & " " & _s_close
                                .SLOT = 30
                                .WAIT = txtAutoForce.Text
                            End With


                            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
                            eng.SaveAutoForce(_ShopID, uPara.USER_PARA.ID, p)
                        Next

                    End If

                    DateFrom = DateAdd(DateInterval.Day, 1, DateFrom)
                End While

            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert(" & ex.Message.ToString & " );", True)
                Exit Sub
            End Try
            Config.SaveTransLog("บันทึกข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopAutoForce.aspx.btnSave_Click", Config.GetLoginHistoryID)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Save Complete');", True)
        End If


    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        'Dim IsSelect As Boolean = False
        'For i As Integer = 0 To chkListTimeSlot.Items.Count - 1
        '    If chkListTimeSlot.Items(i).Selected Then
        '        IsSelect = True
        '        Exit For
        '    End If
        'Next

        'If IsSelect = False Then
        '    ret = False
        '    Config.ShowAlert("Please select time", Me)
        'Else
        If txtDateFrom.Text = "" Then
            ret = False
            Config.ShowAlert("Please select Date Range", Me)
        ElseIf txtDateTo.Text = "" Then
            ret = False
            Config.ShowAlert("Please select Date Range", Me)
        Else

        End If

        If Engine.Common.FunctionEng.cStrToDate2(txtDateFrom.Text) > Engine.Common.FunctionEng.cStrToDate2(txtDateTo.Text) Then
            ret = False
            Config.ShowAlert("Date Range is wrong.", Me)
        End If

        Return ret
    End Function
End Class
