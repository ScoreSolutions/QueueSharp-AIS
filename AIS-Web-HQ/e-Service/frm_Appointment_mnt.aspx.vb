Imports System
Imports System.Data
Imports System.Globalization
Imports System.Data.OleDb
Imports System.Drawing

Partial Class frm_Appointment_mnt
    Inherits System.Web.UI.Page

    Private ReadOnly Property strDateNow() As String
        Get
            Return Date.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en").DateTimeFormat)
        End Get
    End Property
    Dim dtshop As New DataTable
    Dim drshop As DataRow
    'Dim objConn As New OleDbConnection(ConfigurationManager.ConnectionStrings("QisDB").ConnectionString)
    Dim globalFunction As New globalFunction

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
        MasterPageFile = cPara.MasterPage
        cPara = Nothing
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
            txtNetworkType.Text = cPara.NetworkType
            txtPreferLang.Text = cPara.PreferLang
            bindObj(cPara)
            BindRegion(cPara)
            BindTooltip(cPara)
            'BindShop(cPara)
            BindServiceAll(cPara, dtpservice1, "'0'")

            Dim dDt As New DataTable
            dDt.Columns.Add("id")
            dDt.Columns.Add("Item_Name_Th")
            dDt.Columns.Add("Item_Name")
            dDt.Columns.Add("Item_Order")

            Dim dDr As DataRow = dDt.NewRow
            dDr("id") = "0"
            dDr("Item_Name_Th") = "<-- กรุณาเลือก -->"
            dDr("Item_Name") = "<-- Select -->"
            dDr("Item_Order") = "0"
            dDt.Rows.Add(dDr)

            If cPara.PreferLang = "Thai" Then
                dtpservice2.DataTextField = "item_name_th"
                dtpservice3.DataTextField = "item_name_th"
            Else
                dtpservice2.DataTextField = "item_name"
                dtpservice3.DataTextField = "item_name"
            End If
            dtpservice2.DataValueField = "id"
            dtpservice2.DataSource = dDt
            dtpservice2.DataBind()

            dtpservice3.DataValueField = "id"
            dtpservice3.DataSource = dDt
            dtpservice3.DataBind()

            Dim ArrCheckService As New ArrayList
            ArrCheckService.Add("Service1:0")
            ArrCheckService.Add("Service2:0")
            ArrCheckService.Add("Service3:0")

            If Session("ArrService") Is Nothing Then
                Session("ArrService") = ArrCheckService
            End If

            Dim arrshop As Array
            Dim arrservice As Array
            If Not Session("serviceid") Is Nothing Then
                arrservice = Session("serviceid").Split("|")
                '###### ตั้มเพิ่ม กรณีเลือก service เดียว ด้วย ########
                If UBound(arrservice) = 0 Then
                    If Not arrservice(0) Is Nothing Then
                        dtpservice1.SelectedValue = arrservice(0)
                        BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                        ArrCheckService(0) = "Service1:" & arrservice(0)
                        lblService1Default.Text = dtpservice1.SelectedValue
                    End If
                End If
                '##########################################
                If UBound(arrservice) = 1 Then
                    If Not arrservice(0) Is Nothing Then
                        dtpservice1.SelectedValue = arrservice(0)
                        BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                        ArrCheckService(0) = "Service1:" & arrservice(0)
                        lblService1Default.Text = dtpservice1.SelectedValue
                    End If
                    '###### ตั้มเพิ่ม ให้ดึง Service 2 ด้วย ########
                    If Not arrservice(1) Is Nothing Then
                        dtpservice2.SelectedValue = arrservice(1)
                        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
                        ArrCheckService(1) = "Service2:" & arrservice(1)
                        lblService2Default.Text = dtpservice2.SelectedValue
                    End If
                    '##########################333########
                End If
                If UBound(arrservice) = 2 Then
                    If Not arrservice(0) Is Nothing Then
                        dtpservice1.SelectedValue = arrservice(0)
                        BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                        ArrCheckService(0) = "Service1:" & arrservice(0)
                        lblService1Default.Text = dtpservice1.SelectedValue
                    End If
                    If Not arrservice(1) Is Nothing Then
                        dtpservice2.SelectedValue = arrservice(1)
                        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
                        ArrCheckService(1) = "Service2:" & arrservice(1)
                        lblService2Default.Text = dtpservice2.SelectedValue
                    End If
                    If Not arrservice(2) Is Nothing Then
                        dtpservice3.SelectedValue = arrservice(2)
                        ArrCheckService(2) = "Service3:" & arrservice(2)
                        lblService3Default.Text = dtpservice3.SelectedValue
                    End If
                End If
                If UBound(arrservice) = 3 Then
                    If Not arrservice(0) Is Nothing Then
                        dtpservice1.SelectedValue = arrservice(0)
                        BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                        ArrCheckService(0) = "Service1:" & arrservice(0)
                        lblService1Default.Text = dtpservice1.SelectedValue

                    End If
                    If Not arrservice(1) Is Nothing Then
                        dtpservice2.SelectedValue = arrservice(1)
                        '###จะทำให้การ Defalut ค่าไม่ข้ามเพราะ ค่า Defalut เป็น 0 จะดีงค่า  Defalut  ถัดไป
                        If dtpservice2.SelectedValue = 0 Then
                            dtpservice2.SelectedValue = arrservice(2)
                        End If
                        '##################
                        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
                        ArrCheckService(1) = "Service2:" & arrservice(1)
                        lblService2Default.Text = dtpservice2.SelectedValue

                    End If
                    If Not arrservice(2) Is Nothing Then
                        dtpservice3.SelectedValue = arrservice(2)
                        ArrCheckService(2) = "Service3:" & arrservice(2)
                        lblService3Default.Text = dtpservice3.SelectedValue

                    End If
                End If
                Session("ArrService") = ArrCheckService
                'CheckServiceChoosed("1")
            Else
                If Not Request.QueryString("service") Is Nothing Then
                    Session("serviceid") = Request.QueryString("service").ToString().Replace(",", "|")
                    arrservice = Request.QueryString("service").ToString().Split(",")
                    If UBound(arrservice) = 0 Then
                        If Not arrservice(0) Is Nothing Then
                            dtpservice1.SelectedValue = arrservice(0)
                            BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                            ArrCheckService(0) = "Service1:" & arrservice(0)
                        End If
                    End If
                    If UBound(arrservice) = 1 Then
                        If Not arrservice(0) Is Nothing Then
                            dtpservice1.SelectedValue = arrservice(0)
                            BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                            ArrCheckService(0) = "Service1:" & arrservice(0)
                        End If
                        If Not arrservice(1) Is Nothing Then
                            dtpservice2.SelectedValue = arrservice(1)
                            BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
                            ArrCheckService(1) = "Service2:" & arrservice(1)
                        End If
                    End If
                    If UBound(arrservice) = 2 Then
                        If Not arrservice(0) Is Nothing Then
                            dtpservice1.SelectedValue = arrservice(0)
                            BindServiceAll(cPara, dtpservice2, "'" & dtpservice1.SelectedValue & "'")
                            ArrCheckService(0) = "Service1:" & arrservice(0)
                        End If
                        If Not arrservice(1) Is Nothing Then
                            dtpservice2.SelectedValue = arrservice(1)
                            BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
                            ArrCheckService(1) = "Service2:" & arrservice(1)
                        End If
                        If Not arrservice(2) Is Nothing Then
                            dtpservice3.SelectedValue = arrservice(2)
                            ArrCheckService(2) = "Service3:" & arrservice(2)
                        End If
                    End If
                End If
                Session("ArrService") = ArrCheckService
                'CheckServiceChoosed("1")
            End If

            UpdateDataBindShopRegion(cPara, strDateNow)



            If Not Session("region_id") Is Nothing Then
                dtpregion.SelectedValue = Session("region_id")
                'SetShopList()
            End If
            If Not Request.QueryString("shop_id") Is Nothing Then
                Session("shop_id") = Request.QueryString("shop_id").ToString()
                arrshop = Session("shop_id").ToString.Split("|")

                Dim rDt As DataTable = Engine.Common.FunctionEng.GetDatatable("Select Region_ID From TB_Shop Where ID='" & arrshop(0) & "'")
                dtpregion.SelectedValue = rDt.Rows(0)("Region_ID")
                Session("region_id") = rDt.Rows(0)("Region_ID")
                rDt.Dispose()
            End If
            If dtpregion.SelectedValue <> "0" Then
                If Not Session("shop_id") Is Nothing Then
                    SetShopList()
                    dtpbranch.SelectedValue = Session("shop_id").ToString.Split("|")(0)
                End If
            Else
                SetShopList()
            End If

            Dim appdt As Date
            If Session("Appointment") Is Nothing Then
                If Not Request.QueryString("appdt") Is Nothing Then
                    appdt = Request.QueryString("appdt")
                    Session("Appointment") = appdt.ToString("yyyy-MM-dd", New CultureInfo("en-US")) & "|" & appdt.ToString("HH:mm", New CultureInfo("en-US")) & "|" & appdt.ToString("dd/M/yyyy")
                    Session("ShopidOld") = dtpbranch.SelectedValue
                    Session("ShopidNew") = dtpbranch.SelectedValue
                Else
                    Session("Appointment") = ""
                    Session("ShopidOld") = "0"
                End If
            End If
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
            lbl_headder1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_headder1")
            lbl_headder2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_headder2")
            lbl_type.Text = globalFunction.GetText(cPara.PreferLang, "lbl_type")
            lbl_service1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_service1")
            lbl_service2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_service2")
            lbl_service3.Text = globalFunction.GetText(cPara.PreferLang, "lbl_service3")
            lbl_region.Text = globalFunction.GetText(cPara.PreferLang, "lbl_region")
            lbl_branch.Text = globalFunction.GetText(cPara.PreferLang, "lbl_branch")
            img_ok.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_ok")

            If Request("shop_id") IsNot Nothing Then
                img_return.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return")
                img_return.Visible = True
                Session("edit_click") = True
            ElseIf Not IsNothing(Session("edit_click")) Then 'แก้ไม่ให้ปุ่ม ย้อนกลับหาย
                img_return.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return")
                img_return.Visible = True
            End If

            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString

            lbl_headder1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_headder2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_type.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_service1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_service2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_service3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_region.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_branch.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))

            dtpservice1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            dtpservice2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            dtpservice3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            dtpregion.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            dtpbranch.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))

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
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try

    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_return.ToolTip = "ย้อนกลับ"
            img_ok.ToolTip = "ตกลง"
            img_tab1.ToolTip = "นัดรับบริการ"
            img_tab2.ToolTip = "ประวัติการรับบริการ"
            dtpservice1.ToolTip = "บริการที่ 1"
            dtpservice2.ToolTip = "บริการที่ 2"
            dtpservice3.ToolTip = "บริการที่ 3"
            dtpregion.ToolTip = "ภาค"
            dtpbranch.ToolTip = "สาขา"
        Else
            img_return.ToolTip = "Back"
            img_ok.ToolTip = "Ok"
            img_tab1.ToolTip = "Appointment"
            img_tab2.ToolTip = "History"
            dtpservice1.ToolTip = "Service 1"
            dtpservice2.ToolTip = "Service 2"
            dtpservice3.ToolTip = "Service 3"
            dtpregion.ToolTip = "Region"
            dtpbranch.ToolTip = "Branch"
        End If
    End Sub

    'Private Sub BindType(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
    '    Try

    '        Dim dt As New DataTable
    '        dt = globalFunction.GetRegion("")

    '        If dt.Rows.Count > 0 Then

    '            If cPara.PreferLang = "Thai" Then
    '                dtpregion.DataTextField = "region_name_th"
    '            Else
    '                dtpregion.DataTextField = "region_name_en"
    '            End If
    '            dtptype1.DataValueField = "id"
    '            dtptype1.DataSource = dt
    '            dtptype1.DataBind()
    '            dtptype1.SelectedIndex = 0
    '            dtptype1.DataValueField = "id"
    '            dtptype2.DataSource = dt
    '            dtptype2.DataBind()
    '            dtptype2.SelectedIndex = 0
    '            dtptype3.DataValueField = "id"
    '            dtptype3.DataSource = dt
    '            dtptype3.DataBind()
    '            dtptype3.SelectedIndex = 0
    '        End If

    '    Catch ex As Exception
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Bindregion Error !!,Please Check Function');", True)
    '    End Try
    'End Sub

    Private Sub BindShop(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("Shop_Code")
        dt.Columns.Add("Shop_Name_Th")
        dt.Columns.Add("Shop_Name_En")
        dt.Columns.Add("Region_ID")

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("Shop_Code") = "0000"
        dr("Shop_Name_Th") = "<-- กรุณาเลือก -->"
        dr("Shop_Name_En") = "<-- Select -->"
        dr("Region_ID") = "0"
        dt.Rows.Add(dr)

        If cPara.PreferLang = "Thai" Then
            dtpbranch.DataTextField = "shop_name_th"
        Else
            dtpbranch.DataTextField = "shop_name_en"
        End If
        dtpbranch.DataValueField = "id"
        dtpbranch.DataSource = dt
        dtpbranch.DataBind()
    End Sub

    Private Sub BindRegion(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("region_id")
            dt.Columns.Add("Region_Name_Th")
            dt.Columns.Add("Region_Name_En")

            'dt = globalFunction.GetRegion("")

            Dim dr As DataRow = dt.NewRow
            dr("region_id") = "0"
            dr("Region_Name_Th") = "<-- กรุณาเลือก -->"
            dr("Region_Name_En") = "<-- Select -->"
            dt.Rows.Add(dr)
            If dt.Rows.Count > 0 Then
                If cPara.PreferLang = "Thai" Then
                    dtpregion.DataTextField = "region_name_th"
                Else
                    dtpregion.DataTextField = "region_name_en"
                End If
                dtpregion.DataValueField = "region_id"
                dtpregion.DataSource = dt
                dtpregion.DataBind()
                dtpregion.SelectedIndex = 0
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Bindregion Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Function dtRegion() As DataTable
        Try
            Dim eng As New Engine.Appointment.AppointmentENG
            Dim RegionAllListDt As DataTable = eng.GetRegionAllList()

            Dim dt As New DataTable
            dt.Columns.Add("id")
            dt.Columns.Add("region_id")
            dt.Columns.Add("Region_Name_Th")
            dt.Columns.Add("Region_Name_En")

            'dt = globalFunction.GetRegion("")

            'Dim dr As DataRow = dt.NewRow
            'dr("region_id") = "0"
            'dr("Region_Name_Th") = "<-- กรุณาเลือก -->"
            'dr("Region_Name_En") = "<-- Select -->"
            'dt.Rows.Add(dr)

            Dim dr As DataRow = dt.NewRow
            For i As Integer = 0 To RegionAllListDt.Rows.Count - 1
                dr = dt.NewRow
                dr("id") = RegionAllListDt.Rows.Item(i)("id") & ""
                dr("region_id") = RegionAllListDt.Rows.Item(i)("id") & ""
                dr("Region_Name_Th") = RegionAllListDt.Rows.Item(i)("Region_Name_Th") & ""
                dr("Region_Name_En") = RegionAllListDt.Rows.Item(i)("Region_Name_En") & ""
                dt.Rows.Add(dr)
            Next

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub BindShopByRegion(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            Dim Dt As New DataTable
            Dt = globalFunction.GetShop(dtpregion.SelectedValue, "")
            If cPara.PreferLang = "Thai" Then
                dtpbranch.DataTextField = "shop_name_th"
            Else
                dtpbranch.DataTextField = "shop_name_en"
            End If
            dtpbranch.DataValueField = "id"
            dtpbranch.DataSource = Dt
            dtpbranch.DataBind()

            Dim eng As New Engine.Appointment.AppointmentENG
            Dim pl As CenParaDB.Appointment.LastShopRegisterPara = eng.GetLastShopRegister(cPara.MobileNo)
            If pl.SHOP_ID.ToString = "0" Then
                If Not Dt Is Nothing Then
                    dtpbranch.SelectedIndex = 0
                Else
                    dtpbranch.Items.Clear()
                End If
            Else
                dtpbranch.SelectedValue = pl.SHOP_ID.ToString
            End If
            pl = Nothing
            eng = Nothing
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Bindregion Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Sub BindServiceAll(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara, ByVal cmbService As DropDownList, ByVal NotInID As String)
        Try
            'Dim dt As New DataTable
            'dt = globalFunction.GetService
            'Dim strService1 As String = ""
            'Dim strService2 As String = ""
            'Dim strService3 As String = ""

            'If (dtpservice1.SelectedValue <> "") And (dtpservice1.SelectedValue IsNot Nothing) Then
            '    strService1 = dtpservice1.SelectedValue
            'End If
            'If (dtpservice2.SelectedValue <> "") And (dtpservice2.SelectedValue IsNot Nothing) Then
            '    strService2 = dtpservice2.SelectedValue
            'End If
            'If (dtpservice3.SelectedValue <> "") And (dtpservice3.SelectedValue IsNot Nothing) Then
            '    strService3 = dtpservice3.SelectedValue
            'End If

            'If cPara.PreferLang = "Thai" Then
            '    dtpservice1.DataTextField = "item_name_th"
            '    dtpservice2.DataTextField = "item_name_th"
            '    dtpservice3.DataTextField = "item_name_th"
            'Else
            '    dtpservice1.DataTextField = "item_name"
            '    dtpservice2.DataTextField = "item_name"
            '    dtpservice3.DataTextField = "item_name"
            'End If

            'dtpservice1.DataValueField = "id"
            'dtpservice1.DataSource = dt
            'dtpservice1.DataBind()
            'dtpservice2.DataValueField = "id"
            'dtpservice2.DataSource = dt
            'dtpservice2.DataBind()
            'dtpservice3.DataValueField = "id"
            'dtpservice3.DataSource = dt
            'dtpservice3.DataBind()

            'If strService1 <> "" Then dtpservice1.SelectedValue = strService1
            'If strService2 <> "" Then dtpservice2.SelectedValue = strService2
            'If strService3 <> "" Then dtpservice3.SelectedValue = strService3


            Dim MaxAppointmentDay As String = Engine.Common.FunctionEng.GetQisDBConfig("MaxAppointmentDay")
            If MaxAppointmentDay.Trim = "" Then
                MaxAppointmentDay = "7"
            End If

            Dim strDateFrom As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim strDateTo As String = DateAdd(DateInterval.Day, Convert.ToInt16(MaxAppointmentDay), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim dt As New DataTable
            If Val(dtpbranch.SelectedValue) = 0 Or NotInID = "0" Then
                dt = globalFunction.GetServiceAgent_ByDate(NotInID, strDateFrom, strDateTo)
            Else
                dt = globalFunction.GetServiceByShopAgent_ByDate(NotInID, GetMaster_ItemidServiceByShop(strDateNow, Val(dtpbranch.SelectedValue)), strDateFrom, strDateTo)
            End If
            'dt = globalFunction.GetService(NotInID)
            'dt = globalFunction.GetServiceByShop(NotInID, GetMaster_ItemidServiceByShop)
            If cPara.PreferLang = "Thai" Then
                cmbService.DataTextField = "item_name_th"
            Else
                cmbService.DataTextField = "item_name"
            End If

            cmbService.DataValueField = "id"
            cmbService.DataSource = dt
            cmbService.DataBind()

            dt.Dispose()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindServiceAll Error !!,Please Check Function');", True)
        End Try
    End Sub

    Protected Sub img_ok_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ok.Click
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()

            If dtpservice1.SelectedValue = "0" And dtpservice2.SelectedValue = "0" And dtpservice3.SelectedValue = "0" Then
                If cPara.PreferLang.ToLower = "thai" Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('กรุณาเลือกบริการอย่างน้อย 1 บริการ');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please select at least one service');", True)
                End If
                Exit Sub
            End If

            If dtpregion.SelectedValue = "0" Then
                If cPara.PreferLang.ToLower = "thai" Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('กรุณาเลือกภาค');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please select region');", True)
                End If
                Exit Sub
            End If

            If dtpbranch.SelectedValue = "0" Then
                If cPara.PreferLang.ToLower = "thai" Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('กรุณาเลือกสาขา');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please select shop');", True)
                End If
                Exit Sub
            End If

            Session("service1") = dtpservice1.SelectedValue & "|" & dtpservice1.SelectedItem.Text
            Session("service2") = dtpservice2.SelectedValue & "|" & dtpservice2.SelectedItem.Text
            Session("service3") = dtpservice3.SelectedValue & "|" & dtpservice3.SelectedItem.Text

            Session("serviceitemtime") = SetItemTime(dtpservice1.SelectedValue & "," & dtpservice2.SelectedValue & "," & dtpservice3.SelectedValue)

            Dim SelectedServiceID As String = dtpservice1.SelectedValue & "|"
            If dtpservice2.SelectedValue <> "0" Then
                SelectedServiceID += dtpservice2.SelectedValue & "|"
            End If
            If dtpservice3.SelectedValue <> "0" Then
                SelectedServiceID += dtpservice3.SelectedValue & "|"
            End If
            Session("serviceid") = SelectedServiceID
            Session("shop_id") = dtpbranch.SelectedValue
            Session("region_id") = dtpregion.SelectedValue

            If Session("Edit") = "" Then
                Response.Redirect("frm_AddAppointment.aspx?shop_id=" & dtpbranch.SelectedValue & "|" & dtpbranch.SelectedItem.Text)
            Else
                Response.Redirect("frm_AddAppointment.aspx?shop_id=" & dtpbranch.SelectedValue & "|" & dtpbranch.SelectedItem.Text)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function SetItemTime(ByVal id As String) As String
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
        Dim cEng As New Engine.Appointment.AppointmentENG
        Dim dt As DataTable = cEng.GetItemTime(id)
        Dim strService As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1

            If strService = "" Then
                strService = dt(i)("id")
            Else
                strService &= "," & dt(i)("id")
            End If

        Next

        cPara = Nothing
        cEng = Nothing

        Return strService
    End Function


    Protected Sub dtpregion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpregion.SelectedIndexChanged
        'SetShopList()
        '######### ตั้มเพิ่ม เก็บค่าที่เคยเลือก 2013/06/03 ########
        lblRegionDefault.Text = dtpregion.SelectedValue
        PoplateShopListDefault()
        '############################################

        PopulateServiceByShop()

        SetDefaultService()

        'End If
        '##เก็บ Shopid ใหม่เมื่อมีการเลือก Shop ใหม่
        Session("ShopidNew") = dtpbranch.SelectedValue
    End Sub

    Private Sub PopulateDefaultService()
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()

        BindServiceAll(cPara, dtpservice1, "'0'")

        Dim dDt As New DataTable
        dDt.Columns.Add("id")
        dDt.Columns.Add("Item_Name_Th")
        dDt.Columns.Add("Item_Name")
        dDt.Columns.Add("Item_Order")

        Dim dDr As DataRow = dDt.NewRow
        dDr("id") = "0"
        dDr("Item_Name_Th") = "<-- กรุณาเลือก -->"
        dDr("Item_Name") = "<-- Select -->"
        dDr("Item_Order") = "0"
        dDt.Rows.Add(dDr)

        If cPara.PreferLang = "Thai" Then
            dtpservice2.DataTextField = "item_name_th"
            dtpservice3.DataTextField = "item_name_th"
        Else
            dtpservice2.DataTextField = "item_name"
            dtpservice3.DataTextField = "item_name"
        End If
        dtpservice2.DataValueField = "id"
        dtpservice2.DataSource = dDt
        dtpservice2.DataBind()

        dtpservice3.DataValueField = "id"
        dtpservice3.DataSource = dDt
        dtpservice3.DataBind()

        cPara = Nothing
    End Sub

    Protected Sub dtpbranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpbranch.SelectedIndexChanged
        '######### ตั้มเพิ่ม เก็บค่าที่เคยเลือก 2013/06/03 ########
        lblBranchDefault.Text = dtpbranch.SelectedValue
        '############################################
        PopulateServiceByShop()

        SetDefaultService()
        '##เก็บ Shopid ใหม่เมื่อมีการเลือก Shop ใหม่
        Session("ShopidNew") = dtpbranch.SelectedValue

    End Sub
    

    Protected Sub dtpservice1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpservice1.SelectedIndexChanged
        '        If dtpservice1.SelectedValue = "0" Then GoTo Checked
        '        If dtpservice1.SelectedValue <> dtpservice2.SelectedValue And dtpservice1.SelectedValue <> dtpservice3.SelectedValue Then
        'Checked:
        '            Dim Arr As ArrayList = Session("ArrService")
        '            Arr.Item(0) = "Service1:" & dtpservice1.SelectedValue
        '            Session("ArrService") = Arr
        '            CheckServiceChoosed("1")
        '        Else
        '            Dim Arr As ArrayList = Session("ArrService")
        '            dtpservice1.SelectedValue = Arr(0).ToString.Replace("Service1:", "")
        '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Service is Duplicate,Please try again');", True)
        '        End If



        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindServiceAll(cPara, dtpservice2, dtpservice1.SelectedValue)
        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
        UpdateDataBindShopRegion(cPara, strDateNow)
        cPara = Nothing

        '######### ตั้มเพิ่ม เก็บค่าที่เคยเลือกและดึงมา Set Default   2013/06/03 ########
        lblService1Default.Text = dtpservice1.SelectedValue
        PoplateShopListDefault()
        SetDefaultService()
        '############################################
    End Sub

    Protected Sub dtpservice2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpservice2.SelectedIndexChanged
        '        If dtpservice2.SelectedValue = "0" Then GoTo Checked
        '        If dtpservice2.SelectedValue <> dtpservice1.SelectedValue And dtpservice2.SelectedValue <> dtpservice3.SelectedValue Then
        'Checked:
        '            Dim Arr As ArrayList = Session("ArrService")
        '            Arr.Item(1) = "Service2:" & dtpservice2.SelectedValue
        '            Session("ArrService") = Arr
        '            CheckServiceChoosed("2")
        '        Else
        '            Dim Arr As ArrayList = Session("ArrService")
        '            dtpservice2.SelectedValue = Arr(1).ToString.Replace("Service2:", "")
        '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Service is Duplicate,Please try again');", True)
        '        End If
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin

        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
        UpdateDataBindShopRegion(cPara, strDateNow)
        cPara = Nothing

        '######### ตั้มเพิ่ม เก็บค่าที่เคยเลือกและดึงมา Set Default   2013/06/03 ########
        lblService2Default.Text = dtpservice2.SelectedValue
        PoplateShopListDefault()
        SetDefaultService()
        '############################################
    End Sub

    Protected Sub dtpservice3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpservice3.SelectedIndexChanged
        '        If dtpservice3.SelectedValue = "0" Then GoTo Checked
        '        If dtpservice3.SelectedValue <> dtpservice1.SelectedValue And dtpservice3.SelectedValue <> dtpservice2.SelectedValue Then
        'Checked:
        '            Dim Arr As ArrayList = Session("ArrService")
        '            Arr.Item(2) = "Service3:" & dtpservice3.SelectedValue
        '            Session("ArrService") = Arr
        '            CheckServiceChoosed("3")
        '        Else
        '            Dim Arr As ArrayList = Session("ArrService")
        '            dtpservice3.SelectedValue = Arr(2).ToString.Replace("Service3:", "")
        '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Service is Duplicate,Please try again');", True)
        '        End If
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        UpdateDataBindShopRegion(cPara, strDateNow)
        cPara = Nothing

        '######### ตั้มเพิ่ม เก็บค่าที่เคยเลือกและดึงมา Set Default   2013/06/03 ########
        lblService3Default.Text = dtpservice3.SelectedValue
        PoplateShopListDefault()
        SetDefaultService()
        '############################################
    End Sub

    Private Sub PoplateShopListDefault()
        'Check ว่าค่าที่เคยเลือกไว้  ถ้ามี ให้ Default ค่านั้นเลย

        'If lblService1Default.Text <> "" Then
        '    Try
        '        dtpservice1.SelectedValue = lblService1Default.Text
        '    Catch ex As Exception
        '        dtpservice1.SelectedIndex = 0
        '    End Try
        'End If

        'If lblService2Default.Text <> "" Then
        '    Try
        '        dtpservice2.SelectedValue = lblService2Default.Text
        '    Catch ex As Exception
        '        dtpservice2.SelectedIndex = 0
        '    End Try
        'End If


        'If lblService3Default.Text <> "" Then
        '    Try
        '        dtpservice3.SelectedValue = lblService3Default.Text
        '    Catch ex As Exception
        '        dtpservice3.SelectedIndex = 0
        '    End Try
        'End If


        If lblRegionDefault.Text <> "" Then
            Try
                dtpregion.SelectedValue = lblRegionDefault.Text
            Catch ex As Exception
                dtpregion.SelectedIndex = 0
            End Try
        End If
        SetShopList()
        If lblBranchDefault.Text <> "" Then
            Try
                dtpbranch.SelectedValue = lblBranchDefault.Text
            Catch ex As Exception
                dtpbranch.SelectedIndex = 0
            End Try
        End If

        'Clear ค่าที่เลยเลือก
        ClearValueDefault()
    End Sub

    Private Sub ClearValueDefault()
        lblService1Default.Text = IIf(Val(Me.dtpservice1.SelectedValue) = 0, "", Val(Me.dtpservice1.SelectedValue))
        lblService2Default.Text = IIf(Val(Me.dtpservice2.SelectedValue) = 0, "", Val(Me.dtpservice2.SelectedValue))
        lblService3Default.Text = IIf(Val(Me.dtpservice3.SelectedValue) = 0, "", Val(Me.dtpservice3.SelectedValue))
        lblRegionDefault.Text = IIf(Val(Me.dtpregion.SelectedValue) = 0, "", Val(Me.dtpregion.SelectedValue))
        lblBranchDefault.Text = IIf(Val(Me.dtpbranch.SelectedValue) = 0, "", Val(Me.dtpbranch.SelectedValue))
    End Sub


    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

    Protected Sub img_tab1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab1.Click
        Response.Redirect("frm_DontReservStt.aspx")
    End Sub

    Function CheckServiceChoosed(ByVal strDdrID As String) As Boolean
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            Dim Dt As New DataTable
            Dim strServiceID As String = ""
            Dim ArrSer As New ArrayList
            If Session("ArrService") IsNot Nothing Then
                ArrSer = Session("ArrService")
                For j As Integer = 0 To ArrSer.Count - 1
                    Dim strID As String = ""
                    strID = ArrSer(j).ToString.Replace("Service" & j + 1 & ":", "")
                    If strID <> "0" Then strServiceID &= "'" & ArrSer(j).ToString.Replace("Service" & j + 1 & ":", "") & "',"
                Next
            End If

            'Dt = globalFunction.GetService()
            'Dim Arr As Array = Dt.Select("Id not in (" & strServiceID.Substring(0, strServiceID.Length - 1) & ")", "Item_Order")
            Dim Dr As DataRow() = Dt.Select("Id not in (" & strServiceID.Substring(0, strServiceID.Length - 1) & ")", "Item_Order")
            Dim Dtable As New DataTable
            Dtable = Dt.Copy
            Dtable.Clear()
            For Each Drr As DataRow In Dr
                Dtable.ImportRow(Drr)
            Next


            'Dtable.Columns.Add("ID")
            'Dtable.Columns.Add("Item_Name_Th")
            'Dtable.Columns.Add("Item_Name")
            'For i As Integer = 0 To Arr.Length - 1
            '    Dr = Dtable.NewRow
            '    Dr.Item("ID") = Arr(i)(0)
            '    Dr.Item("Item_Name_Th") = Arr(i)(1)
            '    Dr.Item("Item_Name") = Arr(i)(2)
            '    Dtable.Rows.Add(Dr)
            'Next

            If cPara.PreferLang = "Thai" Then
                dtpservice1.DataTextField = "item_name_th"
                dtpservice2.DataTextField = "item_name_th"
                dtpservice3.DataTextField = "item_name_th"
            Else
                dtpservice1.DataTextField = "item_name"
                dtpservice2.DataTextField = "item_name"
                dtpservice3.DataTextField = "item_name"
            End If



            If ArrSer IsNot Nothing Then
                If strDdrID <> "1" And (strDdrID = "2" Or strDdrID = "3") Then
                    If ArrSer(0).ToString.Replace("Service1:", "") = "0" Then
Service1:
                        dtpservice1.DataSource = Dtable
                        dtpservice1.DataBind()
                        dtpservice1.SelectedValue = ArrSer(0).ToString.Replace("Service1:", "")
                    End If
                Else
                    If ArrSer(0).ToString.Replace("Service1:", "") = "0" Then GoTo Service1
                End If
                If strDdrID <> "2" And (strDdrID = "1" Or strDdrID = "3") Then
                    If ArrSer(1).ToString.Replace("Service2:", "") = "0" Then
Service2:
                        dtpservice2.DataSource = Dtable
                        dtpservice2.DataBind()
                        dtpservice2.SelectedValue = ArrSer(1).ToString.Replace("Service2:", "")
                    End If
                Else
                    If ArrSer(1).ToString.Replace("Service2:", "") = "0" Then GoTo Service2
                End If
                If strDdrID <> "3" And (strDdrID = "1" Or strDdrID = "2") Then
                    If ArrSer(2).ToString.Replace("Service3:", "") = "0" Then
Service3:
                        dtpservice3.DataSource = Dtable
                        dtpservice3.DataBind()
                        dtpservice3.SelectedValue = ArrSer(2).ToString.Replace("Service3:", "")
                    End If
                Else
                    If ArrSer(2).ToString.Replace("Service3:", "") = "0" Then GoTo Service3
                End If

                For j As Integer = 0 To ArrSer.Count - 1
                    Dim strValue As String = ArrSer(j).ToString.Replace("Service" & j + 1 & ":", "")
                    Dim DTemp As New DataTable
                    DTemp = Dtable.Copy
                    Dim Drow As DataRow
                    Dim ArrTemp As Array
                    Dim DDTemp As New DataTable

                    If strValue <> "0" Then ArrTemp = Dt.Select("ID='" & strValue & "'") Else ArrTemp = Nothing
                    Try
                        If j = 0 And strDdrID <> "1" Then
                            If strValue <> "0" Then
                                Drow = DTemp.NewRow
                                Drow.Item("ID") = ArrTemp(0)(0)
                                Drow.Item("Item_Name_Th") = ArrTemp(0)(1)
                                Drow.Item("Item_Name") = ArrTemp(0)(2)
                                Drow.Item("Item_Order") = ArrTemp(0)(3)
                                DTemp.Rows.Add(Drow)
                                Dim Drr As DataRow() = DTemp.Select("", "Item_Order")
                                DDTemp = DTemp.Copy
                                DDTemp.Clear()

                                For Each DrTemp As DataRow In Drr
                                    DDTemp.ImportRow(DrTemp)
                                Next
                                dtpservice1.DataSource = DDTemp
                                dtpservice1.DataBind()
                            Else
                                dtpservice1.DataSource = DTemp
                                dtpservice1.DataBind()
                            End If
                            dtpservice1.SelectedValue = strValue
                            DTemp.Clear()
                        ElseIf j = 1 And strDdrID <> "2" Then

                            If strValue <> "0" Then
                                Drow = DTemp.NewRow
                                Drow.Item("ID") = ArrTemp(0)(0)
                                Drow.Item("Item_Name_Th") = ArrTemp(0)(1)
                                Drow.Item("Item_Name") = ArrTemp(0)(2)
                                Drow.Item("Item_Order") = ArrTemp(0)(3)
                                DTemp.Rows.Add(Drow)
                                Dim Drr As DataRow() = DTemp.Select("", "Item_Order")
                                DDTemp = DTemp.Copy
                                DDTemp.Clear()
                                'DTemp.Clear()
                                For Each DrTemp As DataRow In Drr
                                    DDTemp.ImportRow(DrTemp)
                                Next
                                dtpservice2.DataSource = DDTemp
                                dtpservice2.DataBind()

                            Else
                                dtpservice2.DataSource = DTemp
                                dtpservice2.DataBind()
                            End If
                            dtpservice2.SelectedValue = strValue
                            DTemp.Clear()
                        ElseIf j = 2 And strDdrID <> "3" Then

                            If strValue <> "0" Then
                                Drow = DTemp.NewRow
                                Drow.Item("ID") = ArrTemp(0)(0)
                                Drow.Item("Item_Name_Th") = ArrTemp(0)(1)
                                Drow.Item("Item_Name") = ArrTemp(0)(2)
                                Drow.Item("Item_Order") = ArrTemp(0)(3)
                                DTemp.Rows.Add(Drow)
                                Dim Drr As DataRow() = DTemp.Select("", "Item_Order")
                                DDTemp = DTemp.Copy
                                DDTemp.Clear()
                                'DTemp.Clear()
                                For Each DrTemp As DataRow In Drr
                                    DDTemp.ImportRow(DrTemp)
                                Next
                                dtpservice3.DataSource = DDTemp
                                dtpservice3.DataBind()
                            Else
                                dtpservice3.DataSource = DTemp
                                dtpservice3.DataBind()
                            End If
                            dtpservice3.SelectedValue = strValue
                            DTemp.Clear()
                        End If

                        ArrTemp = Nothing
                    Catch ex As Exception

                    Finally

                    End Try
                Next
            End If
            cPara = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function CheckServiceInShop() As ArrayList
        Try
            Dim Dt As DataTable = Engine.Common.FunctionEng.GetActiveShop()
            Dim ArrShopID As New ArrayList
            For Each Item As DataRow In Dt.Rows
                Try
                    Dim strServiceID As String = ""
                    Dim intCnt As Integer = 0
                    If dtpservice1.SelectedValue <> "0" Then strServiceID &= "'" & dtpservice1.SelectedValue & "'," : intCnt += 1
                    If dtpservice2.SelectedValue <> "0" Then strServiceID &= "'" & dtpservice2.SelectedValue & "'," : intCnt += 1
                    If dtpservice3.SelectedValue <> "0" Then strServiceID &= "'" & dtpservice3.SelectedValue & "'," : intCnt += 1

                    Dim sSql As String = "Select Count(*) qty From TB_Item Where 1=1" & IIf(strServiceID <> "", " And ID In (" & strServiceID.Substring(0, strServiceID.Length - 1) & ")", "")
                    Dim sDt As DataTable = Engine.Common.FunctionEng.ExecuteShopSQL(sSql, Item("id"), "frm_Appointment_mnt_CheckServiceInShop")
                    If sDt.Rows(0)("qty") = intCnt Then ArrShopID.Add(Item("ID") & "|" & Item("Region_ID"))
                Catch ex As Exception

                End Try
            Next
            Return ArrShopID
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Sub UpdateDataBindShopRegion(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara, ByVal DateNow As String)
        Dim Sdt As New DataTable
        Dim eng As New Engine.Appointment.AppointmentENG
        '##### ตั้มแก้ให้ ดึง Region ทั้งหมด ถ้ายังไม่เลือก Service ใดๆเลย #####
        If dtpservice1.SelectedIndex = 0 And dtpservice2.SelectedIndex = 0 And dtpservice3.SelectedIndex = 0 Then
            Sdt = dtRegion()
            '###################################################
        Else
            'Sdt = eng.GetShopByService("'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "','" & dtpservice3.SelectedValue & "'")
            Sdt = eng.GetShopByService_ByDate("'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "','" & dtpservice3.SelectedValue & "'", DateNow)
        End If
        If Sdt.Rows.Count > 0 Then
            Dim Rdt As New DataTable
            Rdt = Sdt.DefaultView.ToTable(True, "region_id", "region_name_th", "region_name_en")

            Dim DefaultRegionID As String = "0"
            If Rdt.Rows.Count = 1 Then
                DefaultRegionID = Rdt.Rows(0)("region_id")
            End If

            Dim Rdr As DataRow = Rdt.NewRow
            Rdr("region_id") = "0"
            Rdr("region_name_th") = "<-- กรุณาเลือก -->"
            Rdr("region_name_en") = "<-- Select -->"

            Rdt.Rows.InsertAt(Rdr, 0)

            If cPara.PreferLang = "Thai" Then
                dtpregion.DataTextField = "region_name_th"
            Else
                dtpregion.DataTextField = "region_name_en"
            End If
            dtpregion.DataValueField = "region_id"
            dtpregion.DataSource = Rdt
            dtpregion.DataBind()
            dtpregion.SelectedValue = DefaultRegionID

            Rdt.Dispose()

            SetShopList()
        Else
            Dim Rdt As New DataTable
            Rdt.Columns.Add("region_id")
            Rdt.Columns.Add("region_name_th")
            Rdt.Columns.Add("region_name_en")

            Dim Rdr As DataRow = Rdt.NewRow
            Rdr("region_id") = "0"
            Rdr("region_name_th") = "<-- กรุณาเลือก -->"
            Rdr("region_name_en") = "<-- Select -->"

            Rdt.Rows.InsertAt(Rdr, 0)

            If cPara.PreferLang = "Thai" Then
                dtpregion.DataTextField = "region_name_th"
            Else
                dtpregion.DataTextField = "region_name_en"
            End If
            dtpregion.DataValueField = "region_id"
            dtpregion.DataSource = Rdt
            dtpregion.DataBind()
            Rdt.Dispose()
            SetShopList()
        End If
        Sdt.Dispose()
        eng = Nothing
    End Sub

    Private Sub SetShopList()
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        'BindShop(cPara)
        '#### ตั้มเพิ่ม
        If dtpbranch.Items.Count = 0 Then
            BindShop(cPara)
        ElseIf dtpregion.SelectedIndex = 0 Then
            BindShop(cPara)
        End If
        If dtpregion.SelectedValue > 0 Then
            Dim eng As New Engine.Appointment.AppointmentENG
            Dim shDt As DataTable = eng.GetShopByService("'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "','" & dtpservice3.SelectedValue & "'")
            ''##### ตั้มแก้ให้ ดึง Region ทั้งหมด ถ้ายังไม่เลือก Service ใดๆเลย #####
            'If dtpservice1.SelectedIndex = 0 And dtpservice2.SelectedIndex = 0 And dtpservice3.SelectedIndex = 0 Then
            '    shDt = dtRegion()
            'Else
            '    shDt = eng.GetShopByService("'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "','" & dtpservice3.SelectedValue & "'")
            'End If

            If shDt.Rows.Count > 0 Then
                shDt.DefaultView.RowFilter = "region_id='" & dtpregion.SelectedValue & "'"

                Dim DefaultShopID As String = "0"
                If shDt.DefaultView.Count = 1 Then
                    DefaultShopID = shDt.DefaultView(0)("id")
                End If

                Dim tmp As New DataTable
                tmp.Columns.Add("id")
                tmp.Columns.Add("shop_name_th")
                tmp.Columns.Add("shop_name_en")

                Dim dr As DataRow = tmp.NewRow
                dr("id") = "0"
                dr("shop_name_th") = "<-- กรุณาเลือก -->"
                dr("shop_name_en") = "<-- Select -->"
                tmp.Rows.InsertAt(dr, 0)

                For Each tDr As DataRowView In shDt.DefaultView
                    dr = tmp.NewRow
                    dr("id") = tDr("id")
                    dr("shop_name_th") = tDr("shop_name_th")
                    dr("shop_name_en") = tDr("shop_name_en")
                    tmp.Rows.Add(dr)
                Next

                dtpbranch.DataSource = tmp
                dtpbranch.DataBind()
                dtpbranch.SelectedValue = DefaultShopID
            Else
                dtpbranch.DataSource = Nothing
                dtpbranch.DataBind()
            End If
            shDt.Dispose()
            eng = Nothing
        End If
        cPara = Nothing
    End Sub


    Protected Sub img_return_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_return.Click
        Response.Redirect("frm_DontReservStt.aspx")
    End Sub


#Region "Function"
    Private Function GetMaster_ItemidServiceByShop(ByVal strDataNow As String, ByVal strShopId As String) As String
        Dim strId As String = ""

        Dim MaxAppointmentDay As String = Engine.Common.FunctionEng.GetQisDBConfig("MaxAppointmentDay")
        If MaxAppointmentDay.Trim = "" Then
            MaxAppointmentDay = "7"
        End If

        Dim strDateFrom As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        Dim strDateTo As String = DateAdd(DateInterval.Day, Convert.ToInt16(MaxAppointmentDay), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

        Dim dt As New DataTable
        dt = globalFunction.GetServiceAgent_ByDateAndShop(0, strDateFrom, strDateTo, strShopId)
        For i As Integer = 0 To dt.Rows.Count - 1
            If strId = "" Then
                strId = dt.Rows.Item(i)("id")
            Else
                strId &= "," & dt.Rows.Item(i)("id")
            End If
        Next
        dt = Nothing
        'Dim eng As New Engine.Appointment.AppointmentENG
        'Dim strMaster_Itemid As String = eng.GetMaster_ItemidServiceByShop(Me.dtpbranch.SelectedValue, strId)
        'eng = Nothing
        'Return  strMaster_Itemid

        Return strId
    End Function

    Private Sub PopulateServiceByShop()
        '######### ตั้มเพิ่ม ดึงข้อมูล Service จาก Shop    2013/06/04 ########
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        BindServiceAll(cPara, dtpservice1, "'0'")
        If lblService1Default.Text <> "" Then
            Try
                dtpservice1.SelectedValue = lblService1Default.Text
            Catch ex As Exception
                dtpservice1.SelectedIndex = 0
            End Try
        End If
        BindServiceAll(cPara, dtpservice2, dtpservice1.SelectedValue)
        If lblService2Default.Text <> "" Then
            Try
                dtpservice2.SelectedValue = lblService2Default.Text
            Catch ex As Exception
                dtpservice2.SelectedIndex = 0
            End Try
        End If
        BindServiceAll(cPara, dtpservice3, "'" & dtpservice1.SelectedValue & "','" & dtpservice2.SelectedValue & "'")
        If lblService3Default.Text <> "" Then
            Try
                dtpservice3.SelectedValue = lblService3Default.Text
            Catch ex As Exception
                dtpservice3.SelectedIndex = 0
            End Try
        End If
        cPara = Nothing

    End Sub
    Private Sub SetDefaultService()
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin

        Dim dDt As New DataTable
        dDt.Columns.Add("id")
        dDt.Columns.Add("Item_Name_Th")
        dDt.Columns.Add("Item_Name")
        dDt.Columns.Add("Item_Order")

        Dim dDr As DataRow = dDt.NewRow
        dDr("id") = "0"
        dDr("Item_Name_Th") = "<-- กรุณาเลือก -->"
        dDr("Item_Name") = "<-- Select -->"
        dDr("Item_Order") = "0"
        dDt.Rows.Add(dDr)

        If dtpservice1.SelectedValue = 0 Then
            If cPara.PreferLang = "Thai" Then
                dtpservice2.DataTextField = "item_name_th"
                dtpservice3.DataTextField = "item_name_th"
            Else
                dtpservice2.DataTextField = "item_name"
                dtpservice3.DataTextField = "item_name"
            End If
            dtpservice2.DataValueField = "id"
            dtpservice2.DataSource = dDt
            dtpservice2.DataBind()

            dtpservice3.DataValueField = "id"
            dtpservice3.DataSource = dDt
            dtpservice3.DataBind()
        ElseIf dtpservice1.SelectedValue <> 0 And dtpservice2.SelectedValue = 0 Then
            If cPara.PreferLang = "Thai" Then
                dtpservice3.DataTextField = "item_name_th"
            Else
                dtpservice3.DataTextField = "item_name"
            End If
            dtpservice3.DataValueField = "id"
            dtpservice3.DataSource = dDt
            dtpservice3.DataBind()
        End If




        '      If dtpregion.SelectedValue <> 0 And dtpbranch.SelectedValue <> 0 Then

        'ElseIf dtpregion.SelectedValue = 0 And dtpbranch.SelectedValue = 0 Then


        'ElseIf dtpregion.SelectedValue <> 0 And dtpbranch.SelectedValue = 0 Then


        'End If


        'If dtpservice1.SelectedValue <> 0 And dtpservice2.SelectedValue = 0 And dtpservice3.SelectedValue = 0 Then
        '    If cPara.PreferLang = "Thai" Then
        '        dtpservice3.DataTextField = "item_name_th"
        '    Else
        '        dtpservice3.DataTextField = "item_name"
        '    End If
        '    dtpservice3.DataValueField = "id"
        '    dtpservice3.DataSource = dDt
        '    dtpservice3.DataBind()
        'ElseIf dtpregion.SelectedValue <> 0 And dtpbranch.SelectedValue <> 0 And dtpservice1.SelectedValue = 0 And dtpservice2.SelectedValue = 0 And dtpservice3.SelectedValue = 0 Then
        '    If cPara.PreferLang = "Thai" Then
        '        dtpservice2.DataTextField = "item_name_th"
        '        dtpservice3.DataTextField = "item_name_th"
        '    Else
        '        dtpservice2.DataTextField = "item_name"
        '        dtpservice3.DataTextField = "item_name"
        '    End If
        '    dtpservice2.DataValueField = "id"
        '    dtpservice2.DataSource = dDt
        '    dtpservice2.DataBind()

        '    dtpservice3.DataValueField = "id"
        '    dtpservice3.DataSource = dDt
        '    dtpservice3.DataBind()
        'ElseIf dtpregion.SelectedValue <> 0 And dtpbranch.SelectedValue = 0 And dtpservice1.SelectedValue = 0 And dtpservice2.SelectedValue = 0 And dtpservice3.SelectedValue = 0 Then
        '    If cPara.PreferLang = "Thai" Then
        '        dtpservice2.DataTextField = "item_name_th"
        '        dtpservice3.DataTextField = "item_name_th"
        '    Else
        '        dtpservice2.DataTextField = "item_name"
        '        dtpservice3.DataTextField = "item_name"
        '    End If
        '    dtpservice2.DataValueField = "id"
        '    dtpservice2.DataSource = dDt
        '    dtpservice2.DataBind()

        '    dtpservice3.DataValueField = "id"
        '    dtpservice3.DataSource = dDt
        '    dtpservice3.DataBind()
        'ElseIf dtpregion.SelectedValue <> 0 And dtpbranch.SelectedValue <> 0 And dtpservice1.SelectedValue <> 0 And dtpservice2.SelectedValue = 0 And dtpservice3.SelectedValue = 0 Then
        '    If cPara.PreferLang = "Thai" Then
        '        dtpservice2.DataTextField = "item_name_th"
        '        dtpservice3.DataTextField = "item_name_th"
        '    Else
        '        dtpservice2.DataTextField = "item_name"
        '        dtpservice3.DataTextField = "item_name"
        '    End If
        '    dtpservice2.DataValueField = "id"
        '    dtpservice2.DataSource = dDt
        '    dtpservice2.DataBind()

        '    dtpservice3.DataValueField = "id"
        '    dtpservice3.DataSource = dDt
        '    dtpservice3.DataBind()
        'End If

        cPara = Nothing
    End Sub
#End Region
End Class
