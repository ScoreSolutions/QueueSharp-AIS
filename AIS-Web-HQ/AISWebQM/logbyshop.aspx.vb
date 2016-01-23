Imports System.Data
Imports CenLinqDB.Common.Utilities
Imports CenLinqDB.TABLE

Partial Class logbyshop
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")
    Dim MyUser As utils.User
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim utl As New utils
        If Request("shopid") = "" Then
            Response.Redirect("select_shop.aspx?nextpage=logbyshop.aspx")
        End If
        Session("shopid") = Request("shopid")

        If Not IsPostBack Then
            Dim shLnq As New TbShopCenLinqDB
            Session.Remove("SearchResult")
            BindPage()

            shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(Request("shopid"))
            lblShopName.Text = shLnq.SHOP_NAME_EN & " (" & shLnq.SHOP_NAME_TH & ")"

            'Dim dt As New DataTable
            'dt = utl.GetShopList(Master.Conn, Request("shopid"))

            'If dt.Rows.Count > 0 Then
            '    lblShopName.Text = dt.Rows(0)("shop_name_en") & IIf(dt.Rows(0)("shop_name_th").ToString <> "", " (" & dt.Rows(0)("shop_name_th") & ")", "")
            'End If
            'ServDate.SelectedDate = Date.Today
            BindSearch()

            '### ตั้ม เพิ่ม ให้เก็บ IP ของ Shop  เพื่อใช้เป็น Link###
            'HiddenField_ShopWebQM_IP.Value = ShopWebQM_IP()
            'HiddenField_User_IP.Value = Request.UserHostAddress ' User_IP()
            'shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(Request("shopid"))
            'If shLnq.ID <> 0 Then
            '    lblShopName.Text = shLnq.SHOP_NAME_EN & "(" & shLnq.SHOP_NAME_TH & ")"
            '    HiddenField_ShopWebQM_IP.Value = shLnq.SHOP_QM_URL
            '    HiddenField_DcWebQM_IP.Value = shLnq.MAIN_SERVERNAME
            '    ' Session("IsExtanalIP") = IsExtanalIP()
            'End If
            'shLnq = Nothing
        End If
        Title = Version

        gvAgent.PageSize = ddlPage.SelectedValue
        gvAgentNew.PageSize = ddlPage.SelectedValue
    End Sub

    Sub BindPage()
        For i As Integer = 10 To 50 Step 10
            ddlPage.Items.Add(i)
        Next
    End Sub

    Sub BindSearch()
        Dim utl As New utils
        Dim it As New ListItem("-- All --", "")

        Dim dtA As New DataTable
        dtA = utl.GetAgentList(Request("shopid"))
        If dtA.Rows.Count > 0 Then
            ddlAgent.DataSource = dtA
            ddlAgent.DataValueField = "userid"
            ddlAgent.DataTextField = "username"
            ddlAgent.DataBind()
            ddlAgent.Items.Insert(0, it)
            dtA = Nothing
        End If

        Dim dtS As New DataTable
        dtS = utl.GetServiceList(Request("shopid"))
        If dtS.Rows.Count > 0 Then
            ddlService.DataSource = dtS
            ddlService.DataValueField = "item_id"
            ddlService.DataTextField = "item_name"
            ddlService.DataBind()
            ddlService.Items.Insert(0, it)
            dtS = Nothing
        End If

        Dim dtC As New DataTable
        dtC = utl.GetCounterList(Request("shopid"))
        If dtC.Rows.Count > 0 Then
            ddlCounterName.DataSource = dtC
            ddlCounterName.DataValueField = "id"
            ddlCounterName.DataTextField = "counter_name"
            ddlCounterName.DataBind()
            ddlCounterName.Items.Insert(0, it)
            dtC = Nothing
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        gvAgentNew.Visible = False
        gvAgent.Visible = True
        Dim utl As New utils
        If txtDateF.DateValue.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Start Date !!')</script>", False)
            Exit Sub
        End If
        If txtDateT.DateValue.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก End Date !!')</script>", False)
            Exit Sub
        End If
        If txtDateF.DateValue > txtDateT.DateValue Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก  Start Date มากกว่า End Date !!')</script>", False)
            Exit Sub
        End If
        If ddlFrom.SelectedValue <> "0" And ddlTo.SelectedValue <> "0" Then
            If ddlFrom.SelectedValue > ddlTo.SelectedValue Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Time from มากกว่า Time to')</script>", False)
                Exit Sub
            End If
        End If


        lblDate.Visible = True
        lblDate.Text = "Date : " & txtDateF.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & txtDateT.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))

        Dim dt As New DataTable
        dt = utl.GetServiceLog(Request("shopid"), ddlAgent.SelectedItem.Value, ddlService.SelectedValue, tbxQ.Text, tbxMobile.Text, ddlFrom.SelectedValue, ddlTo.SelectedValue, SetFormatDate(txtDateF.DateValue), SetFormatDate(txtDateT.DateValue), ddlCounterName.SelectedValue)
        SetGridview(dt, True)
        dt.Dispose()
    End Sub

    Private Sub SetGridview(ByVal dt As DataTable, ByVal IsClickSearch As Boolean)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "service_date,item_name"
            dt = dt.DefaultView.ToTable

            If dt.Columns.Contains("no") = False Then
                dt.Columns.Add("no")
            End If
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = (i + 1)
            Next

            If IsClickSearch = True Then
                gvAgent.PageIndex = 0
            End If

            gvAgent.DataSource = dt
            gvAgent.DataBind()
            Session("SearchResult") = dt
        Else
            gvAgent.DataSource = Nothing
            gvAgent.DataBind()
            Session.Remove("SearchResult")
        End If
    End Sub

    Private Function SetFormatDate(ByVal txtDate As String) As String
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        If IsDate(txtDate) Then
            Dim dmy As Date = CDate(txtDate)
            d = dmy.Day
            m = dmy.Month
            y = dmy.Year
            If y > 2500 Then
                y = y - 543
            End If
            Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
        Else
            Return ""
        End If
    End Function

    'Protected Sub btnSearchAllShop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchAllShop.Click
    '    gvAgentNew.Visible = True
    '    gvAgent.Visible = False
    '    Dim utl As New utils
    '    Dim dt As New DataTable
    '    Dim ST As String = ""
    '    Dim ET As String = ""
    '    If ddlFrom.SelectedIndex > 0 Then
    '        ST = ddlFrom.Text
    '    End If
    '    If ddlTo.SelectedIndex > 0 Then
    '        ET = ddlTo.Text
    '    End If

    '    If txtDateF.DateValue.Year = "1" Then
    '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date from !!')</script>", False)
    '        Exit Sub
    '    End If
    '    If txtDateT.DateValue.Year = "1" Then
    '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date to !!')</script>", False)
    '        Exit Sub
    '    End If
    '    If txtDateF.DateValue > txtDateT.DateValue Then
    '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Date from มากกว่า Date to !!')</script>", False)
    '        Exit Sub
    '    End If

    '    gvAgentNew.DataSource = utl.GetServiceLogAllShop(ddlAgent.SelectedItem.Value, ddlService.SelectedValue, tbxQ.Text, tbxMobile.Text, Master.Conn, SetFormatDate(txtDateF.DateValue), SetFormatDate(txtDateT.DateValue), ST, ET)
    '    gvAgentNew.DataBind()
    'End Sub



    Protected Sub gvAgent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAgent.PageIndexChanging
        gvAgent.PageIndex = e.NewPageIndex
        'btnSearch_Click(Nothing, Nothing)
        SetGridview(Session("SearchResult"), False)
    End Sub

    Protected Sub gvAgentNew_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAgentNew.PageIndexChanging
        gvAgentNew.PageIndex = e.NewPageIndex
        'btnSearchAllShop_Click(Nothing, Nothing)
    End Sub

    Protected Sub gvAgent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgent.RowDataBound
        '#### ตั้มเพิ่ม ให้เปิด popup video ให้ดึง จาก Shop ####'

        'If Not IsNothing(hplShowVideo) Then
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplShowVideo As HyperLink = e.Row.FindControl("hplShowVideo")
            Dim lblIsFile As Label = e.Row.FindControl("lblIsFile")
            If Not IsNothing(hplShowVideo) Then
                If e.Row.DataItem("qm_status") = 4 Then
                    Dim strid As String = e.Row.DataItem("id") '95 '
                    Dim fdate As String = e.Row.DataItem("file_date") '130826 '
                    Dim rnd As String = DateTime.Now.Millisecond '95
                    'HiddenField_ShopWebQM_IP.Value = "localhost:55692"


                    'If hidShopUseQM.Value = "Y" Then
                    '    Dim strUrlParameter As String = "http://" & HiddenField_ShopWebQM_IP.Value & "/ShopWebQM/Video.aspx?shopid=" & Request.QueryString("shopid") & _
                    '    "&qid=" & strid & _
                    '    "&fdate=" & fdate & _
                    '    "&rnd=" & strid & _
                    '    "&ip=" & HiddenField_ShopWebQM_IP.Value
                    '    Dim popup = "windowPopup('" & strUrlParameter & "'," & _
                    '            "'ShopWebQM'," & _
                    '            "'center:yes;resizable:no;dialogWidth:800px;dialogHeight:500px;scrollbars:0;location:0;directories:0;status:0;menubar:0;');return false;"
                    '    '"'width=800, height=500, resizable=yes,scrollbars=yes,location=no');return false;"
                    '    If IsExtanalIP() = False Then 'Check ว่าเป็น IP ภายนอก Shop,True นอก Shop False ใน Shop 
                    '        hplShowVideo.Attributes.Add("onClick", popup)
                    '        ' hplShowVideo.NavigateUrl = "vdo.aspx?qid=" & strid & "&fdate=" & fdate & "&rnd=" & DateTime.Now.Millisecond

                    '    Else
                    '        hplShowVideo.NavigateUrl = "vdo.aspx?qid=" & strid & "&fdate=" & fdate & "&rnd=" & DateTime.Now.Millisecond
                    '    End If
                    'Else
                    '    hplShowVideo.NavigateUrl = "vdo.aspx?qid=" & strid & "&fdate=" & fdate & "&rnd=" & DateTime.Now.Millisecond
                    'End If

                    hplShowVideo.NavigateUrl = "vdo.aspx?qid=" & strid & "&fdate=" & fdate & "&shopid=" & Request.QueryString("shopid")
                    lblIsFile.Text = e.Row.DataItem("qm_status_name") & "" '  "Ready"
                Else
                    lblIsFile.Text = e.Row.DataItem("qm_status_name") & "" '"Incomplete"
                    hplShowVideo.Style.Remove("Cursor")
                    hplShowVideo.ToolTip = "Incomplete"
                End If


            End If
        End If

        '#### ตั้มเพิ่ม ให้เปิด popup video ####'
    End Sub

    Private Function ShopWebQM_IP() As String
        Dim dt As New DataTable
        Dim trans As New TransactionDB
        Dim lnq As New TbShopCenLinqDB
        dt = lnq.GetDataList("active = 'Y' and id = " & Request("shopid"), "", trans.Trans)
        dt.TableName = "GetShop"
        trans.CommitTransaction()

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("shop_qm_url")
        Else
            Return "#"
        End If
        'Dim utl As New utils
        'Dim dt As New DataTable
        'dt = utl.GetShopDetail(Request("shopid"), Master.Conn)
        'If dt.Rows.Count > 0 Then
        '    Return dt.Rows(0)("shop_qm_url")
        'Else
        '    Return "#"
        '  End If
    End Function

    Protected Sub gvAgent_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgent.Sorting
        'Dim dt As New DataTable
        'Dim utl As New utils
        'Dim ST As String = ""
        'Dim ET As String = ""
        'If ddlFrom.SelectedIndex > 0 Then
        '    ST = ddlFrom.Text
        'End If
        'If ddlTo.SelectedIndex > 0 Then
        '    ET = ddlTo.Text
        'End If
        'dt = utl.GetServiceLog(Request("shopid"), ddlAgent.SelectedItem.Value, ddlService.SelectedValue, tbxQ.Text, tbxMobile.Text, ST, ET, Master.Conn, SetFormatDate(txtDateF.DateValue), SetFormatDate(txtDateT.DateValue))
        'gvAgent.DataBind()
        'If dt.Rows.Count > 0 Then
        '    GridViewSorting(gvAgent, dt, txtSortDir, txtSortField, e, gvAgent.PageIndex)
        '    gvAgent.DataSource = dt
        '    gvAgent.DataBind()
        'End If

        Dim dt As New DataTable
        dt = Session("SearchResult")
        If dt.Rows.Count > 0 Then
            GridViewSorting(gvAgent, dt, txtSortDir, txtSortField, e, gvAgent.PageIndex)
            gvAgent.DataSource = dt
            gvAgent.DataBind()
        End If

    End Sub

    Public Sub GridViewSorting(ByVal gv As GridView, ByVal dt As DataTable, ByVal txtSortDir As TextBox, ByVal txtSortField As TextBox, ByVal e As GridViewSortEventArgs, ByVal PageIndex As Long)
        If e.SortExpression = "DEFAULT" Then
            txtSortDir.Text = e.SortDirection
            txtSortField.Text = e.SortExpression
        Else
            If txtSortField.Text = e.SortExpression Then
                txtSortDir.Text = IIf(txtSortDir.Text.Trim = "", "DESC", "")
            Else : txtSortField.Text = e.SortExpression
            End If
        End If

        Dim sortStr As String = ""
        If txtSortField.Text.Trim <> "" Then
            sortStr = " " & txtSortField.Text & " " & txtSortDir.Text
        End If

        gv.PageIndex = PageIndex
        dt.DefaultView.Sort = sortStr
        dt = dt.DefaultView.ToTable()
    End Sub

#Region "Get&Check IP"
    Private Function User_IP() As String
        MyUser = CType(Session("MyUser"), utils.User)
        'Dim strHostName As String
        'Dim strIPAddress As String
        'strHostName = System.Net.Dns.GetHostName()
        'strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
        Return MyUser.ip_address
    End Function

    'Private Function IsExtanalIP() As Boolean
    '    Dim ReturnValue As Boolean

    '    Dim UserIP As String() = HiddenField_User_IP.Value.Split(".")
    '    Dim DcIP As String() = HiddenField_DcWebQM_IP.Value.Split(".")
    '    Dim ShopIP As String() = HiddenField_ShopWebQM_IP.Value.Split(".")
    '    Dim UserIP_Split As String = UserIP(0) & "." & UserIP(1) & "." & UserIP(2)
    '    Dim DcIP_Split As String = DcIP(0) & "." & DcIP(1) & "." & DcIP(2)
    '    Dim ShopIP_Split As String = ShopIP(0) & "." & ShopIP(1) & "." & ShopIP(2)

    '    If UserIP_Split = ShopIP_Split Then
    '        ReturnValue = False
    '    Else
    '        ReturnValue = True
    '    End If

    '    UserIP = Nothing
    '    DcIP = Nothing
    '    ShopIP = Nothing
    '    UserIP_Split = Nothing
    '    DcIP_Split = Nothing
    '    ShopIP_Split = Nothing

    '    Return ReturnValue

    'End Function
#End Region

End Class
