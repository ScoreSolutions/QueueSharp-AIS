Imports System
Imports System.Data
Imports System.Globalization
Imports System.Data.OleDb

Partial Class frm_AddAppointment
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction
    Dim blnGrid1 As Boolean = False
    Dim blnGrid2 As Boolean = False
    Dim _checkColorGray As Boolean = False

    Private ReadOnly Property checkColorGray() As String
        Get
            Return _checkColorGray
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request.QueryString("shop_id") Is Nothing Then
                Session("shop_id") = Request.QueryString("shop_id").ToString()
            End If
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            txtNetworkType.Text = cPara.NetworkType
            txtPreferLang.Text = cPara.PreferLang
            BindTimeSlot(cPara)
            bindObj(cPara)
            BindTooltip(cPara)
            btnPrev.Visible = False
            Pnl2.Visible = False

        End If
    End Sub

    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""
            img_tab1.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab1")
            img_tab2.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab2")

            'ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid.ToString, globalFunction.GetStyle(cPara.NetworkType, "tab1", cPara.PreferLang), True)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid.ToString, globalFunction.GetStyle(cPara.NetworkType, "tab2", cPara.PreferLang), True)
            'Page.Form.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "tab1", cPara.PreferLang))
            'Page.Form.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "tab2", cPara.PreferLang))

            lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_time.Text = globalFunction.GetText(cPara.PreferLang, "lbl_time")
            lbl_time2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_time")
            img_return.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return")
            img_next.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_next")
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString

            lbl_message1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_time.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            lbl_time2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))

            td1.Style.Clear() : td1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td2.Style.Clear() : td2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td3.Style.Clear() : td3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td4.Style.Clear() : td4.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td5.Style.Clear() : td5.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td6.Style.Clear() : td6.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td7.Style.Clear() : td7.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td8.Style.Clear() : td8.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td9.Style.Clear() : td9.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td10.Style.Clear() : td10.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td11.Style.Clear() : td11.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td12.Style.Clear() : td12.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td13.Style.Clear() : td13.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td14.Style.Clear() : td14.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            'td15.Style.Clear() : td15.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))

            td16.Style.Clear() : td16.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td17.Style.Clear() : td17.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td18.Style.Clear() : td18.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td19.Style.Clear() : td19.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td20.Style.Clear() : td20.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td21.Style.Clear() : td21.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td22.Style.Clear() : td22.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            td23.Style.Clear() : td23.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table_header"))
            dgvdetail.Style.Clear() : dgvdetail.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "griditem"))
            dgvdetail_2.Style.Clear() : dgvdetail_2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "griditem"))

            If cPara.NetworkType = "pre" Then
                Panel7.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "post" Then
                Panel7.BackImageUrl = "~/image/gsm/bg_2.png"
            ElseIf cPara.NetworkType = "corp" Then
                Panel7.BackImageUrl = "~/image/corp/bg_2.png"
            ElseIf cPara.NetworkType = "3g" Then
                Panel7.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "3gpost" Then
                Panel7.BackImageUrl = "~/image/3gpost/bg_2.png"
            End If
            'tb1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "table"))
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_return.ToolTip = "ย้อนกลับ"
            img_next.ToolTip = "ต่อไป"
            btnNext.ToolTip = "ต่อไป"
            btnPrev.ToolTip = "ย้อนกลับ"
            btnNotAvaliable.ToolTip = "Not Avaliable"
            btnAvaliable.ToolTip = "Avaliable"
        Else
            img_return.ToolTip = "Back"
            img_next.ToolTip = "Next"
            btnNext.ToolTip = "Next"
            btnPrev.ToolTip = "Back"
            lblNotAvaliable.ToolTip = "Not Avaliable"
            lblAvaliable.ToolTip = "Avaliable"
        End If
    End Sub

    Private Sub BindTimeSlot(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            Dim dt As New DataTable
            Dim day1 As Date
            Dim day2 As Date
            Dim day3 As Date
            Dim day4 As Date
            Dim day5 As Date
            Dim day6 As Date
            Dim day7 As Date
            Dim arrshop As Array
            Dim dtshopopen As New DataTable
            Dim dr As DataRow
            dtshopopen.Columns.Add("time")
            dtshopopen.Columns.Add("day1")
            dtshopopen.Columns.Add("day2")
            dtshopopen.Columns.Add("day3")
            dtshopopen.Columns.Add("day4")
            dtshopopen.Columns.Add("day5")
            dtshopopen.Columns.Add("day6")
            dtshopopen.Columns.Add("day7")

            arrshop = Session("shop_id").ToString.Split("|")
            Dim dtshop As New DataTable
            Dim dtslot As New DataTable
            'If objConn.State = ConnectionState.Closed Then objConn.Open()
            'Dim ObjAdapter As New OleDbDataAdapter(, objConn)
            'Dim Ds As New DataTable
            'Ds = Engine.Common.FunctionEng.GetDatatable("Select * From TB_Shop Where ID='" & arrshop(0) & "'")
            'Dim objConnShop As New OleDbConnection
            'ObjAdapter.Fill(Ds, "Dc")

            'With Ds.Rows(0)
            '    objConnShop = globalFunction.GetConnection(.Item("Shop_DB_Server"), .Item("Shop_DB_Name"), .Item("Shop_DB_UserID"), .Item("Shop_DB_PWD"))
            'End With

            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(arrshop(0), "WebAppointment.frm_AddAppointment_BindTimeSlot")
5:
            Dim dDate As Date = Engine.Common.FunctionEng.GetDateNowFromDB()
            Dim strdt As Date
            If dDate.DayOfWeek = DayOfWeek.Sunday Then
                Dim strDate As String = dDate.AddDays(-1).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                strdt = Convert.ToDateTime(Engine.Common.FunctionEng.GetDatatable("select dateadd(d,2-datepart(dw,'" & strDate & "'),'" & strDate & "') date1").Rows(0)("date1"))
            Else
                strdt = Convert.ToDateTime(Engine.Common.FunctionEng.GetDatatable("select dateadd(d,2-datepart(dw,GETDATE()),GETDATE()) date1").Rows(0)("date1"))
            End If

            '****************Generate Time***************************
            Dim slDt As New DataTable
            'If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
            Dim slSql As String = "Select sch.slot, "
            slSql += "      (select top 1 config_value from tb_setting where config_name='s_open') shop_open,"
            slSql += "      (select top 1 config_value from tb_setting where config_name='s_close') shop_close"
            slSql += " From TB_APPOINTMENT_SCHEDULE sch "
            slSql += " where convert(varchar(8),sch.app_date,112)>='" & strdt.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            'Dim ObjAdapter As New OleDbDataAdapter(slSql, objConnShop)
            'ObjAdapter.Fill(slDt)
            'objConnShop.Close()
            slDt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(slSql, shTrans.Trans)
            If slDt.Rows.Count = 0 Then
                _checkColorGray = True 'set ค่า เป็น True เพื่อเก็บไป Check ตอน gen ให้เป็นสีน้ำตาลทั้งหมด 
                'If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
                slSql = "Select 30 as slot, "
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_open') shop_open,"
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_close') shop_close"
                ''slSql += " From TB_APPOINTMENT_SCHEDULE sch Order by id desc "
                '' slSql += " where convert(varchar(8),sch.app_date,112)>='" & DateAdd(DateInterval.Day, -7, strdt).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                'Dim ObjAdapter2 As New OleDbDataAdapter(slSql, objConnShop)
                'ObjAdapter2.Fill(slDt)
                'objConnShop.Close()
                slDt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(slSql, shTrans.Trans)
            End If
            '##################################################################################


            Dim strstarttm As Date
            Dim strendtm As Date
            Dim slot As Integer = 0

            If slDt.Rows.Count > 0 Then
                strstarttm = slDt.Rows(0).Item("shop_open").ToString.Trim
                strendtm = slDt.Rows(0).Item("shop_close").ToString.Trim
                dtslot = slDt
                If dtslot.Rows.Count > 0 Then
                    slot = dtslot.Rows(0).Item("slot").ToString
                End If

                Dim CurrTime As Date = strstarttm
                Do
                    dr = dtshopopen.NewRow
                    dr(0) = CurrTime.ToString("HH:mm")
                    dr(1) = ""
                    dr(2) = ""
                    dr(3) = ""
                    dr(4) = ""
                    dr(5) = ""
                    dr(6) = ""
                    dtshopopen.Rows.Add(dr)

                    CurrTime = DateAdd(DateInterval.Minute, slot, CurrTime)
                Loop While CurrTime < strendtm
            End If
            '**********************************************************

            day1 = strdt
            lbl_day1.Text = Convert.ToDateTime(strdt).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date1.Text = Convert.ToDateTime(strdt).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal1.Text = day1.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day2 = DateAdd(DateInterval.Day, 1, strdt)
            lbl_day2.Text = Convert.ToDateTime(day2).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date2.Text = Convert.ToDateTime(day2).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal2.Text = day2.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day3 = DateAdd(DateInterval.Day, 2, strdt)
            lbl_day3.Text = Convert.ToDateTime(day3).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date3.Text = Convert.ToDateTime(day3).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal3.Text = day3.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day4 = DateAdd(DateInterval.Day, 3, strdt)
            lbl_day4.Text = Convert.ToDateTime(day4).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date4.Text = Convert.ToDateTime(day4).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal4.Text = day4.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day5 = DateAdd(DateInterval.Day, 4, strdt)
            lbl_day5.Text = Convert.ToDateTime(day5).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date5.Text = Convert.ToDateTime(day5).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal5.Text = day5.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day6 = DateAdd(DateInterval.Day, 5, strdt)
            lbl_day6.Text = Convert.ToDateTime(day6).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date6.Text = Convert.ToDateTime(day6).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal6.Text = day6.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day7 = DateAdd(DateInterval.Day, 6, strdt)
            lbl_day7.Text = Convert.ToDateTime(day7).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date7.Text = Convert.ToDateTime(day7).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal7.Text = day7.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            dgvdetail.DataSource = dtshopopen
            dgvdetail.DataBind()


            'Check Time Slot
            Dim ServiceID As String = ""
            If Convert.ToInt16(Split(Session("service1"), "|")(0)) > 0 Then
                ServiceID = Split(Session("service1"), "|")(0)
            End If
            If Convert.ToInt16(Split(Session("service2"), "|")(0)) > 0 Then
                ServiceID += "," & Split(Session("service2"), "|")(0)
            End If
            If Convert.ToInt16(Split(Session("service3"), "|")(0)) > 0 Then
                ServiceID += "," & Split(Session("service3"), "|")(0)
            End If

            Dim CurApp As New DataTable
            Dim TimeDt As New DataTable
            Dim e As New Engine.Appointment.AppointmentENG
            TimeDt = e.CreateTimeSlot(Convert.ToInt64(arrshop(0)), ServiceID)
            If e.GetActiveAppointment(cPara.MobileNo) = True Then
                CurApp = e.GetAppointmentDesc(cPara.MobileNo)
            End If

            e = Nothing
            GenerateTimeSlot(TimeDt, "btn_day1", "lbl_time", lbl_dateorginal1, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day2", "lbl_time", lbl_dateorginal2, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day3", "lbl_time", lbl_dateorginal3, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day4", "lbl_time", lbl_dateorginal4, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day5", "lbl_time", lbl_dateorginal5, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day6", "lbl_time", lbl_dateorginal6, dgvdetail, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day7", "lbl_time", lbl_dateorginal7, dgvdetail, CurApp)
            TimeDt.Dispose()
            CurApp.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindTimeSlot_2(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            Dim dt As New DataTable
            Dim day1 As Date
            Dim day2 As Date
            Dim day3 As Date
            Dim day4 As Date
            Dim day5 As Date
            Dim day6 As Date
            Dim day7 As Date
            Dim arrshop As Array
            Dim dtshopopen As New DataTable
            Dim dr As DataRow
            dtshopopen.Columns.Add("time")
            dtshopopen.Columns.Add("day1")
            dtshopopen.Columns.Add("day2")
            dtshopopen.Columns.Add("day3")
            dtshopopen.Columns.Add("day4")
            dtshopopen.Columns.Add("day5")
            dtshopopen.Columns.Add("day6")
            dtshopopen.Columns.Add("day7")

            arrshop = Session("shop_id").ToString.Split("|")
            Dim dtshop As New DataTable
            Dim dtslot As New DataTable
            Dim Ds As New DataTable
            Ds = Engine.Common.FunctionEng.GetDatatable("Select * From TB_Shop Where ID='" & arrshop(0) & "'")
            Dim objConnShop As New OleDbConnection
            With Ds.Rows(0)
                objConnShop = globalFunction.GetConnection(.Item("Shop_DB_Server"), .Item("Shop_DB_Name"), .Item("Shop_DB_UserID"), .Item("Shop_DB_PWD"))
            End With
5:

            Dim dDate As Date = Engine.Common.FunctionEng.GetDateNowFromDB() 'ObjCmd.ExecuteScalar 'globalFunction.FixDate(ObjCmd.ExecuteScalar)
            Dim strdt As Date
            'หน้าที่ 2 กรณีวันนี้เป็นวันอาทิตย์
            If dDate.DayOfWeek = DayOfWeek.Sunday Then
                Dim strDate As String = dDate.AddDays(-1).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                strdt = Convert.ToDateTime(Engine.Common.FunctionEng.GetDatatable("select dateadd(d,2-datepart(dw,'" & strDate & "'),'" & strDate & "') date1").Rows(0)("date1")) 'ObjCmd.ExecuteScalar
                strdt = strdt.AddDays(7)
            Else
                strdt = Convert.ToDateTime(Engine.Common.FunctionEng.GetDatatable("select dateadd(d,2-datepart(dw,GETDATE()),GETDATE()) date1").Rows(0)("date1")) 'ObjCmd.ExecuteScalar 'globalFunction.FixDate(ObjCmd.ExecuteScalar)
                strdt = strdt.AddDays(7)
            End If

            '****************Generate Time***************************
            Dim slDt As New DataTable
            If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
            Dim slSql As String = "Select sch.slot, "
            slSql += "      (select top 1 config_value from tb_setting where config_name='s_open') shop_open,"
            slSql += "      (select top 1 config_value from tb_setting where config_name='s_close') shop_close"
            slSql += " From TB_APPOINTMENT_SCHEDULE sch "
            slSql += " where convert(varchar(8),sch.app_date,112)>='" & strdt.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            Dim ObjAdapter As New OleDbDataAdapter(slSql, objConnShop)
            ObjAdapter.Fill(slDt)
            objConnShop.Close()

            '########3#ตั้มเพิ่มเงื่อนไง  ถ้ากดมาสัปดาที่ 2 แล้วเป็นหน้าขาว  ให้ดึงสัปดาที่ 1 มา Gen แทน เพื่อให้เกิดเป็น สีน้ำตาล ###

            If slDt.Rows.Count = 0 Then
                _checkColorGray = True 'set ค่า เป็น True เพื่อเก็บไป Check ตอน gen ให้เป็นสีน้ำตาลทั้งหมด 
                If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
                slSql = "Select sch.slot, "
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_open') shop_open,"
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_close') shop_close"
                slSql += " From TB_APPOINTMENT_SCHEDULE sch "
                slSql += " where convert(varchar(8),sch.app_date,112)>='" & DateAdd(DateInterval.Day, -7, strdt).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                Dim ObjAdapter2 As New OleDbDataAdapter(slSql, objConnShop)
                ObjAdapter2.Fill(slDt)
                objConnShop.Close()
            End If

            'กรณีที่สัปดาที่ 1 ไม่ มีข้อมูลให้ดึง(ไม่ genslot) ให้ถึงเวลาเปิด-ปิดมากแสดง   เพื่อให้เกิดเป็น สีน้ำตาล ###
            If slDt.Rows.Count = 0 Then
                _checkColorGray = True 'set ค่า เป็น True เพื่อเก็บไป Check ตอน gen ให้เป็นสีน้ำตาลทั้งหมด 
                If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
                slSql = "Select 30 as slot, "
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_open') shop_open,"
                slSql += "      (select top 1 config_value from tb_setting where config_name='s_close') shop_close"
                '' slSql += " From TB_APPOINTMENT_SCHEDULE sch  Order By id desc"
                Dim ObjAdapter2 As New OleDbDataAdapter(slSql, objConnShop)
                ObjAdapter2.Fill(slDt)
                objConnShop.Close()
            End If
            '##################################################################################
            Dim strstarttm As Date
            Dim strendtm As Date
            Dim slot As Integer = 0

            If slDt.Rows.Count > 0 Then
                strstarttm = slDt.Rows(0).Item("shop_open").ToString.Trim
                strendtm = slDt.Rows(0).Item("shop_close").ToString.Trim
                dtslot = slDt
                If dtslot.Rows.Count > 0 Then
                    slot = dtslot.Rows(0).Item("slot").ToString
                End If

                Dim CurrTime As Date = strstarttm
                Do
                    dr = dtshopopen.NewRow
                    dr(0) = CurrTime.ToString("HH:mm")
                    dr(1) = ""
                    dr(2) = ""
                    dr(3) = ""
                    dr(4) = ""
                    dr(5) = ""
                    dr(6) = ""
                    dtshopopen.Rows.Add(dr)

                    CurrTime = DateAdd(DateInterval.Minute, slot, CurrTime)
                Loop While CurrTime < strendtm
            End If
            '**********************************************************

            day1 = strdt
            lbl_day1_2.Text = Convert.ToDateTime(strdt).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date1_2.Text = Convert.ToDateTime(strdt).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal1_2.Text = day1.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day2 = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(strdt))
            lbl_day2_2.Text = Convert.ToDateTime(day2).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date2_2.Text = Convert.ToDateTime(day2).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal2_2.Text = day2.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) 'Convert.ToDateTime(day2).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day3 = DateAdd(DateInterval.Day, 2, Convert.ToDateTime(strdt))
            lbl_day3_2.Text = Convert.ToDateTime(day3).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date3_2.Text = Convert.ToDateTime(day3).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal3_2.Text = day3.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) 'Convert.ToDateTime(day3).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day4 = DateAdd(DateInterval.Day, 3, Convert.ToDateTime(strdt))
            lbl_day4_2.Text = Convert.ToDateTime(day4).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date4_2.Text = Convert.ToDateTime(day4).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal4_2.Text = day4.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) 'Convert.ToDateTime(day4).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day5 = DateAdd(DateInterval.Day, 4, Convert.ToDateTime(strdt))
            lbl_day5_2.Text = Convert.ToDateTime(day5).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date5_2.Text = Convert.ToDateTime(day5).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal5_2.Text = day5.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) 'Convert.ToDateTime(day5).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day6 = DateAdd(DateInterval.Day, 5, Convert.ToDateTime(strdt))
            lbl_day6_2.Text = Convert.ToDateTime(day6).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date6_2.Text = Convert.ToDateTime(day6).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal6_2.Text = day6.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) ' Convert.ToDateTime(day6).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))

            day7 = DateAdd(DateInterval.Day, 6, Convert.ToDateTime(strdt))
            lbl_day7_2.Text = Convert.ToDateTime(day7).ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_date7_2.Text = Convert.ToDateTime(day7).ToString("dd-MMM", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            lbl_dateorginal7_2.Text = day7.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) 'Convert.ToDateTime(day7).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))


            dgvdetail_2.DataSource = dtshopopen
            dgvdetail_2.DataBind()


            'Check Time Slot
            Dim ServiceID As String = ""
            If Convert.ToInt16(Split(Session("service1"), "|")(0)) > 0 Then
                ServiceID = Split(Session("service1"), "|")(0)
            End If
            If Convert.ToInt16(Split(Session("service2"), "|")(0)) > 0 Then
                ServiceID += "," & Split(Session("service2"), "|")(0)
            End If
            If Convert.ToInt16(Split(Session("service3"), "|")(0)) > 0 Then
                ServiceID += "," & Split(Session("service3"), "|")(0)
            End If

            Dim TimeDt As New DataTable
            Dim CurApp As New DataTable
            Dim e As New Engine.Appointment.AppointmentENG
            TimeDt = e.CreateTimeSlot(Convert.ToInt64(arrshop(0)), ServiceID)
            If e.GetActiveAppointment(cPara.MobileNo) = True Then
                CurApp = e.GetAppointmentDesc(cPara.MobileNo)
            End If
            e = Nothing
            GenerateTimeSlot(TimeDt, "btn_day1_2", "lbl_time2", lbl_dateorginal1_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day2_2", "lbl_time2", lbl_dateorginal2_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day3_2", "lbl_time2", lbl_dateorginal3_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day4_2", "lbl_time2", lbl_dateorginal4_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day5_2", "lbl_time2", lbl_dateorginal5_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day6_2", "lbl_time2", lbl_dateorginal6_2, dgvdetail_2, CurApp)
            GenerateTimeSlot(TimeDt, "btn_day7_2", "lbl_time2", lbl_dateorginal7_2, dgvdetail_2, CurApp)
            TimeDt.Dispose()

            'GetSlot1_2()
            'GetSlot2_2()
            'GetSlot3_2()
            'GetSlot4_2()
            'GetSlot5_2()
            'GetSlot6_2()
            'GetSlot7_2()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GenerateTimeSlot(ByVal TimeDT As DataTable, ByVal btnDay As String, ByVal lblTime As String, ByVal lbl_dateorginal As Label, ByVal Gv As MycustomDG.MyDg, ByVal CurApp As DataTable)
        Try
            Dim StartAppTime As New DateTime(1, 1, 1)
            Dim CurrAppTime As New DateTime(1, 1, 1)
            Dim ServQty As Integer = 0
            If CurApp.Rows.Count > 0 Then
                'กรณีเป็นการแก้ไขรายการจอง
                StartAppTime = CurApp.Rows(0)("slot_datetime")
                ServQty = Split(CurApp.Rows(0)("service_id"), ",").Length
            End If

            Dim dgi As DataGridItem
            If _checkColorGray = True Then 'ถ้าค่าเป็น False ให้เป็นสีน้ำตาลทั้งหมด
                For Each dgi In Gv.Items
                    Dim btn_day As Button = dgi.FindControl(btnDay)
                    btn_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")   'สีน้ำตาล
                    btn_day.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
                    btn_day.Enabled = False
                  
                Next

            Else

                For Each dgi In Gv.Items
                    Dim btn_day As Button = dgi.FindControl(btnDay)
                    Dim lbl_time As Label = dgi.FindControl(lblTime)
                    TimeDT.DefaultView.RowFilter = "ShowDate = '" & lbl_dateorginal.Text & "' and ShowTime = '" & lbl_time.Text & "' and status='ว่าง'"
                    If TimeDT.DefaultView.Count > 0 Then
                        btn_day.BackColor = Drawing.Color.White
                        btn_day.ForeColor = Drawing.Color.White
                        btn_day.Enabled = True
                        btn_day.ToolTip = lbl_time.Text
                    Else
                        btn_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")   'สีน้ำตาล
                        btn_day.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
                        btn_day.Enabled = False
                    End If
                    TimeDT.DefaultView.RowFilter = ""

                    If StartAppTime.Year <> 1 Then
                        CurrAppTime = StartAppTime
                        For i As Integer = 1 To ServQty
                            Dim TmpDate As String = CurrAppTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                            Dim TmpTime As String = CurrAppTime.ToString("HH:mm")
                            If lbl_dateorginal.Text = TmpDate And lbl_time.Text = TmpTime Then
                                btn_day.BackColor = Drawing.Color.White
                                btn_day.ForeColor = Drawing.Color.White
                                btn_day.Enabled = True
                            End If
                            CurrAppTime = DateAdd(DateInterval.Minute, Convert.ToInt64(TimeDT.Rows(0)("SlotMinute")), CurrAppTime)
                        Next
                    End If
                    If Session("Appointment") <> "" Then
                        Dim strapp As Array
                        strapp = Session("Appointment").ToString.Split("|")
                        If lbl_dateorginal.Text = strapp(0) And lbl_time.Text = strapp(1) Then
                            btn_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")   'สีเขียว
                            btn_day.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                            btn_day.ToolTip = lbl_time.Text
                        End If
                    End If
                Next

            End If

            'If dgvdetail_2.Items.Count = 0 Then
            '    Dim dgi2 As DataGridItem
            '    For Each dgi2 In dgvdetail.Items

            '    Next
            'End If

        Catch ex As Exception

        End Try
    End Sub

    '#Region "Get Slot"
    '    Private Sub GetSlot1()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day1 As Button = dgi.FindControl("btn_day1")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal1.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day1.BackColor = Drawing.Color.White
    '                    btn_day1.ForeColor = Drawing.Color.White
    '                    btn_day1.Enabled = True
    '                Else
    '                    btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day1.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array

    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal1.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot1_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day1 As Button = dgi.FindControl("btn_day1_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal1_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day1.BackColor = Drawing.Color.White
    '                    btn_day1.ForeColor = Drawing.Color.White
    '                    btn_day1.Enabled = True
    '                Else
    '                    btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day1.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array

    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal1_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day2 As Button = dgi.FindControl("btn_day2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal2.Text)

    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day2.BackColor = Drawing.Color.White
    '                    btn_day2.ForeColor = Drawing.Color.White
    '                    btn_day2.Enabled = True
    '                Else
    '                    btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day2.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot2_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day2 As Button = dgi.FindControl("btn_day2_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal2_2.Text)

    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                  dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day2.BackColor = Drawing.Color.White
    '                    btn_day2.ForeColor = Drawing.Color.White
    '                    btn_day2.Enabled = True
    '                Else
    '                    btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day2.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal2_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot3()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day3 As Button = dgi.FindControl("btn_day3")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal3.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day3.BackColor = Drawing.Color.White
    '                    btn_day3.ForeColor = Drawing.Color.White
    '                    btn_day3.Enabled = True
    '                Else
    '                    btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day3.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal3.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot3_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day3 As Button = dgi.FindControl("btn_day3_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal3_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day3.BackColor = Drawing.Color.White
    '                    btn_day3.ForeColor = Drawing.Color.White
    '                    btn_day3.Enabled = True
    '                Else
    '                    btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day3.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal3_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot4()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day4 As Button = dgi.FindControl("btn_day4")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal4.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day4.BackColor = Drawing.Color.White
    '                    btn_day4.ForeColor = Drawing.Color.White
    '                    btn_day4.Enabled = True
    '                Else
    '                    btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day4.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal4.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot4_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day4 As Button = dgi.FindControl("btn_day4_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal4_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day4.BackColor = Drawing.Color.White
    '                    btn_day4.ForeColor = Drawing.Color.White
    '                    btn_day4.Enabled = True
    '                Else
    '                    btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day4.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal4_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot5()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day5 As Button = dgi.FindControl("btn_day5")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal5.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day5.BackColor = Drawing.Color.White
    '                    btn_day5.ForeColor = Drawing.Color.White
    '                    btn_day5.Enabled = True
    '                Else
    '                    btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day5.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal5.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot5_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day5 As Button = dgi.FindControl("btn_day5_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal5_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day5.BackColor = Drawing.Color.White
    '                    btn_day5.ForeColor = Drawing.Color.White
    '                    btn_day5.Enabled = True
    '                Else
    '                    btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day5.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal5_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot6()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day6 As Button = dgi.FindControl("btn_day6")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal6.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day6.BackColor = Drawing.Color.White
    '                    btn_day6.ForeColor = Drawing.Color.White
    '                    btn_day6.Enabled = True
    '                Else
    '                    btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day6.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal6.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot6_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day6 As Button = dgi.FindControl("btn_day6_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal6_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day6.BackColor = Drawing.Color.White
    '                    btn_day6.ForeColor = Drawing.Color.White
    '                    btn_day6.Enabled = True
    '                Else
    '                    btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day6.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal6_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot7()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail.Items
    '                Dim btn_day7 As Button = dgi.FindControl("btn_day7")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time")
    '                Dim strdate As Date
    '                strdate = globalFunction.cStrToDate(lbl_dateorginal7.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day7.BackColor = Drawing.Color.White
    '                    btn_day7.ForeColor = Drawing.Color.White
    '                    btn_day7.Enabled = True
    '                Else
    '                    btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day7.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal7.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub GetSlot7_2()
    '        Try
    '            Dim dtshop As New DataTable
    '            Dim arrshop As Array
    '            arrshop = Session("shop_id").ToString.Split("|")
    '            If objConn.State = ConnectionState.Closed Then objConn.Open()
    '            Dim ObjAdapter As New OleDbDataAdapter("Select * From TB_Shop Where ID='" & arrshop(0) & "'", objConn)
    '            Dim Ds As New DataSet
    '            Dim objConnShop As New OleDbConnection
    '            ObjAdapter.Fill(Ds, "Dc")
    '            dtshop = Ds.Tables("Dc")

    '            Dim dgi As DataGridItem
    '            For Each dgi In dgvdetail_2.Items
    '                Dim btn_day7 As Button = dgi.FindControl("btn_day7_2")
    '                Dim lbl_time As Label = dgi.FindControl("lbl_time2")
    '                Dim strdate As Date
    '                strdate = globalFunction.FixDate(lbl_dateorginal7_2.Text)
    '                If globalFunction.RetreiveSlotDate(arrshop(0), dtshop.Rows(0).Item("shop_db_name").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_userid").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_pwd").ToString, _
    '                                    dtshop.Rows(0).Item("shop_db_server").ToString, strdate, lbl_time.Text.ToString) = True Then
    '                    btn_day7.BackColor = Drawing.Color.White
    '                    btn_day7.ForeColor = Drawing.Color.White
    '                    btn_day7.Enabled = True
    '                Else
    '                    btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#948A54")
    '                    btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#948A54")

    '                    btn_day7.Enabled = False
    '                End If
    '                If Session("Appointment") <> "" Then
    '                    Dim strapp As Array
    '                    strapp = Session("Appointment").ToString.Split("|")
    '                    If lbl_dateorginal7_2.Text = strapp(2) And lbl_time.Text = strapp(1) Then
    '                        btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                        btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
    '                    End If

    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '#End Region


    Protected Sub MyButtonClick1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day1 As Button = gr.FindControl("btn_day1")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day1.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal1.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day1.BackColor = Drawing.Color.White
                    btn_day1.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    

    End Sub

    Protected Sub MyButtonClick1_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day1 As Button = gr.FindControl("btn_day1_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day1.BackColor = Drawing.Color.White Then
                    '
                    addjustwhite_2()
                    btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal1_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day1.BackColor = Drawing.Color.White
                    btn_day1.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day2 As Button = gr.FindControl("btn_day2")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day2.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal2.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day2.BackColor = Drawing.Color.White
                    btn_day2.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick2_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day2 As Button = gr.FindControl("btn_day2_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day2.BackColor = Drawing.Color.White Then
                    addjustwhite_2()
                    btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal2_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day2.BackColor = Drawing.Color.White
                    btn_day2.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick3(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day3 As Button = gr.FindControl("btn_day3")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day3.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal3.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day3.BackColor = Drawing.Color.White
                    btn_day3.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick3_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day3 As Button = gr.FindControl("btn_day3_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day3.BackColor = Drawing.Color.White Then


                    addjustwhite_2()

                    btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal3_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day3.BackColor = Drawing.Color.White
                    btn_day3.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick4(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day4 As Button = gr.FindControl("btn_day4")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day4.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal4.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day4.BackColor = Drawing.Color.White
                    btn_day4.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick4_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day4 As Button = gr.FindControl("btn_day4_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day4.BackColor = Drawing.Color.White Then

                    addjustwhite_2()

                    btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal4_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day4.BackColor = Drawing.Color.White
                    btn_day4.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick5(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day5 As Button = gr.FindControl("btn_day5")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day5.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal5.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day5.BackColor = Drawing.Color.White
                    btn_day5.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick5_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day5 As Button = gr.FindControl("btn_day5_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day5.BackColor = Drawing.Color.White Then
                    addjustwhite_2()
                    btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal5_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day5.BackColor = Drawing.Color.White
                    btn_day5.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick6(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        ' If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day6 As Button = gr.FindControl("btn_day6")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day6.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal6.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day6.BackColor = Drawing.Color.White
                    btn_day6.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick6_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day6 As Button = gr.FindControl("btn_day6_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day6.BackColor = Drawing.Color.White Then

                    addjustwhite_2()

                    btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal6_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day6.BackColor = Drawing.Color.White
                    btn_day6.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Protected Sub MyButtonClick7(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid2 Then blnGrid1 = False : Exit Sub
        For Each gr In dgvdetail.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day7 As Button = gr.FindControl("btn_day7")
                Dim lbl_time As Label = gr.FindControl("lbl_time")
                If btn_day7.BackColor = Drawing.Color.White Then
                    addjustwhite()
                    btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal7.Text & "|" & lbl_time.Text
                    'blnGrid1 = True
                Else
                    btn_day7.BackColor = Drawing.Color.White
                    btn_day7.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite_2()
    End Sub

    Protected Sub MyButtonClick7_2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
        Dim itemindex As Integer = gvr.ItemIndex
        Dim gr As DataGridItem
        'If blnGrid1 Then blnGrid2 = False : Exit Sub
        For Each gr In dgvdetail_2.Items
            If gr.ItemIndex = itemindex Then
                Dim btn_day7 As Button = gr.FindControl("btn_day7_2")
                Dim lbl_time As Label = gr.FindControl("lbl_time2")
                If btn_day7.BackColor = Drawing.Color.White Then

                    addjustwhite_2()

                    btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    btn_day7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#87E249")
                    Session("Appointment") = lbl_dateorginal7_2.Text & "|" & lbl_time.Text
                    'blnGrid2 = True
                Else
                    btn_day7.BackColor = Drawing.Color.White
                    btn_day7.ForeColor = Drawing.Color.White
                    Session("Appointment") = ""
                End If
            End If
        Next
        addjustwhite()
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "Script", "document.getElementById('divtb1').style.display = 'none';document.getElementById('divtb2').style.display = 'block';", True)
        Pnl2.Visible = True
    End Sub

    Private Sub addjustwhite()
        Try
            Dim dgi As DataGridItem
            For Each dgi In dgvdetail.Items
                Dim btn_day1 As Button = dgi.FindControl("btn_day1")
                Dim btn_day2 As Button = dgi.FindControl("btn_day2")
                Dim btn_day3 As Button = dgi.FindControl("btn_day3")
                Dim btn_day4 As Button = dgi.FindControl("btn_day4")
                Dim btn_day5 As Button = dgi.FindControl("btn_day5")
                Dim btn_day6 As Button = dgi.FindControl("btn_day6")
                Dim btn_day7 As Button = dgi.FindControl("btn_day7")
                If btn_day1.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day1.BackColor = Drawing.Color.White
                    btn_day1.ForeColor = Drawing.Color.White
                End If
                If btn_day2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day2.BackColor = Drawing.Color.White
                    btn_day2.ForeColor = Drawing.Color.White
                End If
                If btn_day3.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day3.BackColor = Drawing.Color.White
                    btn_day3.ForeColor = Drawing.Color.White
                End If
                If btn_day4.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day4.BackColor = Drawing.Color.White
                    btn_day4.ForeColor = Drawing.Color.White
                End If
                If btn_day5.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day5.BackColor = Drawing.Color.White
                    btn_day5.ForeColor = Drawing.Color.White
                End If
                If btn_day6.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day6.BackColor = Drawing.Color.White
                    btn_day6.ForeColor = Drawing.Color.White
                End If
                If btn_day7.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day7.BackColor = Drawing.Color.White
                    btn_day7.ForeColor = Drawing.Color.White
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub addjustwhite_2()
        Try
            Dim dgi2 As DataGridItem
            For Each dgi2 In dgvdetail_2.Items
                Dim btn_day1_2 As Button = dgi2.FindControl("btn_day1_2")
                Dim btn_day2_2 As Button = dgi2.FindControl("btn_day2_2")
                Dim btn_day3_2 As Button = dgi2.FindControl("btn_day3_2")
                Dim btn_day4_2 As Button = dgi2.FindControl("btn_day4_2")
                Dim btn_day5_2 As Button = dgi2.FindControl("btn_day5_2")
                Dim btn_day6_2 As Button = dgi2.FindControl("btn_day6_2")
                Dim btn_day7_2 As Button = dgi2.FindControl("btn_day7_2")
                If btn_day1_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day1_2.BackColor = Drawing.Color.White
                    btn_day1_2.ForeColor = Drawing.Color.White
                End If
                If btn_day2_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day2_2.BackColor = Drawing.Color.White
                    btn_day2_2.ForeColor = Drawing.Color.White
                End If
                If btn_day3_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day3_2.BackColor = Drawing.Color.White
                    btn_day3_2.ForeColor = Drawing.Color.White
                End If
                If btn_day4_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day4_2.BackColor = Drawing.Color.White
                    btn_day4_2.ForeColor = Drawing.Color.White
                End If
                If btn_day5_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day5_2.BackColor = Drawing.Color.White
                    btn_day5_2.ForeColor = Drawing.Color.White
                End If
                If btn_day6_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day6_2.BackColor = Drawing.Color.White
                    btn_day6_2.ForeColor = Drawing.Color.White
                End If
                If btn_day7_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#87E249") Then
                    btn_day7_2.BackColor = Drawing.Color.White
                    btn_day7_2.ForeColor = Drawing.Color.White
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgvdetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail.PageIndexChanged
        dgvdetail.CurrentPageIndex = e.NewPageIndex
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindTimeSlot(cPara)
        cPara = Nothing
    End Sub

    Protected Sub dgvdetail_2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail_2.PageIndexChanged
        dgvdetail_2.CurrentPageIndex = e.NewPageIndex
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindTimeSlot(cPara)
        cPara = Nothing
    End Sub

    Protected Sub img_return_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_return.Click
        'Session("Appointment") = Nothing
        Response.Redirect("frm_Appointment_mnt.aspx")
    End Sub

    Protected Sub img_next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_next.Click
        Try
            If Session("Appointment") = "" Then
                Dim AlertMsg As String = "Invalid Appointment Time,Please select Appointment again !!"
                Dim cPara As New CenParaDB.Appointment.CustomerWebAppointmentPara
                cPara = globalFunction.GetCustomerLogin
                If cPara.CurrentLang = "th-TH" Then
                    AlertMsg = "กรุณาเลือกเวลาที่ต้องการจอง"
                End If
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & AlertMsg & "');", True)
                cPara = Nothing
                Exit Sub
            Else
                checkservice()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

    Protected Sub img_tab1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab1.Click
        Response.Redirect("frm_Appointment_mnt.aspx")
    End Sub

    Private Sub checkservice()
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            'Dim dtshop As New DataTable
            Dim arrshop As Array
            'Dim arrservice1 As Array
            'Dim arrservice2 As Array
            'Dim arrservice3 As Array


            arrshop = Session("shop_id").ToString.Split("|")
            'dtshop = Engine.Common.FunctionEng.GetDatatable("Select * From TB_Shop Where ID='" & arrshop(0) & "'")

            Dim AppDate() As String = Split(Session("Appointment").ToString, "|")
            If AppDate.Length > 0 Then
                Dim SelectedTime As DateTime = globalFunction.cStrToDateTime(AppDate(0), AppDate(1))

                Dim ServiceID As String = ""
                If Convert.ToInt16(Split(Session("service1"), "|")(0)) > 0 Then
                    ServiceID = Split(Session("service1"), "|")(0)
                End If
                If Convert.ToInt16(Split(Session("service2"), "|")(0)) > 0 Then
                    ServiceID += "," & Split(Session("service2"), "|")(0)
                End If
                If Convert.ToInt16(Split(Session("service3"), "|")(0)) > 0 Then
                    ServiceID += "," & Split(Session("service3"), "|")(0)
                End If

                Dim e As New Engine.Appointment.AppointmentENG
                If e.CheckSlotCapacity(arrshop(0), SelectedTime, cPara.MobileNo, Split(ServiceID, ",").Length) = False Then
                    e = Nothing
                    If cPara.PreferLang = "Thai" Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('ช่วงเวลาไม่พอในการให้บริการ');", True)
                        Exit Sub
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Next Slot for Service 1 not Active,Please try again !!!');", True)
                        Exit Sub
                    End If
                Else
                    e = Nothing
                    Response.Redirect("frm_appointment_confirm.aspx")
                End If


                'Dim objConnShop As New OleDbConnection
                'If globalFunction.RetriveSlot(dtshop.Rows(0).Item("shop_db_name").ToString, dtshop.Rows(0).Item("shop_db_userid").ToString, dtshop.Rows(0).Item("shop_db_pwd").ToString, _
                '                    dtshop.Rows(0).Item("shop_db_server").ToString, SelectedDate, StartSlot, Split(ServiceID, ",").Length, Session("Edit")) = False Then
                '    If cPara.PreferLang = "Thai" Then
                '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('ช่วงเวลาไม่พอในการให้บริการ');", True)
                '        Exit Sub
                '    Else
                '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Next Slot for Service 1 not Active,Please try again !!!');", True)
                '        Exit Sub
                '    End If
                'Else
                '    Response.Redirect("frm_appointment_confirm.aspx")
                'End If




                'If Session("service1") <> "" Then
                '    arrservice1 = Session("service1").ToString.Split("|")
                '    If globalFunction.RetriveSlot(dtshop.Rows(0).Item("shop_db_name").ToString, dtshop.Rows(0).Item("shop_db_userid").ToString, dtshop.Rows(0).Item("shop_db_pwd").ToString, _
                '                        dtshop.Rows(0).Item("shop_db_server").ToString, SelectedDate, StartSlot, 1) = False Then
                '        If cPara.PreferLang = "Thai" Then
                '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('ช่วงเวลาไม่พอในการให้บริการ');", True)
                '            Exit Sub
                '        Else
                '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Next Slot for Service 1 not Active,Please try again !!!');", True)
                '            Exit Sub
                '        End If
                '    Else
                '        Response.Redirect("frm_appointment_confirm.aspx")
                '    End If
                'Else
                '    If Session("service2") <> "" Then
                '        arrservice2 = Session("service2").ToString.Split("|")
                '        If globalFunction.RetriveSlot(dtshop.Rows(0).Item("shop_db_name").ToString, dtshop.Rows(0).Item("shop_db_userid").ToString, dtshop.Rows(0).Item("shop_db_pwd").ToString, _
                '                        dtshop.Rows(0).Item("shop_db_server").ToString, SelectedDate, StartSlot, 2) = False Then

                '            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & Session("Appointment").ToString.Replace("|", " ") & "');", True)
                '            If cPara.PreferLang = "Thai" Then
                '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('ช่วงเวลาไม่พอในการให้บริการ');", True)
                '                Exit Sub
                '            Else
                '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Next Slot for Service 2 not Active,Please try again !!!');", True)
                '                Exit Sub
                '            End If
                '        Else
                '            Response.Redirect("frm_appointment_confirm.aspx")
                '        End If
                '    Else
                '        If Session("service3") <> "" Then
                '            arrservice3 = Session("service3").ToString.Split("|")
                '            If globalFunction.RetriveSlot(dtshop.Rows(0).Item("shop_db_name").ToString, _
                '                            dtshop.Rows(0).Item("shop_db_userid").ToString, _
                '                            dtshop.Rows(0).Item("shop_db_pwd").ToString, _
                '                            dtshop.Rows(0).Item("shop_db_server").ToString, SelectedDate, StartSlot, 3) = False Then
                '                If cPara.PreferLang = "Thai" Then
                '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('ช่วงเวลาไม่พอในการให้บริการ');", True)
                '                    Exit Sub
                '                Else
                '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Next Slot for Service 3 not Active,Please try again !!!');", True)
                '                    Exit Sub
                '                End If
                '            Else
                '                Response.Redirect("frm_appointment_confirm.aspx")
                '            End If
                '        Else
                '            Response.Redirect("frm_appointment_confirm.aspx")
                '        End If
                '    End If
                'End If
            End If
            cPara = Nothing
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        MasterPageFile = cPara.MasterPage
        cPara = Nothing
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        btnNext.Visible = False
        btnPrev.Visible = True

        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindTimeSlot_2(cPara)
        cPara = Nothing
        Pnl1.Visible = False
        Pnl2.Visible = True
    End Sub

    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        btnNext.Visible = True
        btnPrev.Visible = False

        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindTimeSlot(cPara)
        cPara = Nothing
        Pnl1.Visible = True
        Pnl2.Visible = False
    End Sub
End Class
