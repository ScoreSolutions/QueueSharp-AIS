Imports System.Data
Imports System.Data.SqlClient
Partial Class QM
    Inherits System.Web.UI.Page
    Dim itemPerRow As Int16 = 8
    Dim Version As String = utils.GetVersion()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SetListView()
        If IsPostBack = False Then
            SetShopList()
            Title = Version

            Dim LoginHisID As Long = DirectCast(Session("MyUser"), utils.User).login_history_id
            Engine.Common.FunctionEng.SaveTransLog(LoginHisID, "QM.qm.Load", "คลิกเมนู Quality Monitoring")
        End If
    End Sub

    Protected Sub gvShopList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShopList.RowCommand
        If e.CommandName = "Select" Then
            Dim mUser As New utils.User
            mUser = Session("MyUser")

            Dim shPara As New CenParaDB.TABLE.TbShopCenParaDB
            shPara = Engine.Common.FunctionEng.GetTbShopCenParaDB(e.CommandArgument)
            Engine.Common.FunctionEng.SaveTransLog(mUser.login_history_id, "qm.aspx.gvShopList_RowCommand", "เลือก Shop  " & shPara.SHOP_NAME_EN)
            shPara = Nothing
            Server.Transfer("logbyshop.aspx?shopid=" & e.CommandArgument & "&rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub

    Private Sub SetShopList()
        Dim utl As New utils
        Dim mUser As New utils.User
        mUser = Session("MyUser")
        Dim dt As New DataTable
        dt = utl.GetShopListByUser(Master.Conn, mUser.view_others_vdo, mUser.user_id)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = (i + 1)
            Next

            gvShopList.DataSource = dt
            gvShopList.DataBind()
            dt = Nothing
        End If
        mUser = Nothing
    End Sub

    Protected Sub gvShopList_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvShopList.RowEditing

    End Sub
End Class
