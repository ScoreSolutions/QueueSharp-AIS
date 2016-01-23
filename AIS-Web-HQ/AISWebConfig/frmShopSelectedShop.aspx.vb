Imports System.Data
Imports System.Data.SqlClient
Partial Class frmShopSelectedShop
    Inherits System.Web.UI.Page
    Dim itemPerRow As Int16 = 8

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SetListView()
        If IsPostBack = False Then
            If Request("MenuID").ToString <> "" Then
                SetShopList()

                Dim mMenu As New CenParaDB.TABLE.SysmenuCenParaDB
                mMenu = Engine.Common.MenuENG.GetMenuPara(Request("MenuID"))
                If mMenu.ID <> 0 Then
                    lblScreenName.Text = "Shop Setup >> " & mMenu.MENU_NAME_EN & " >> Select Shop"
                End If
                mMenu = Nothing
            Else
                Session.RemoveAll()
                Me.Response.Redirect("frmLogin.aspx")
            End If
            Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopSelectedShop.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Protected Sub gvShopList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShopList.RowCommand
        If e.CommandName = "Select" Then
            Dim mMenu As New CenParaDB.TABLE.SysmenuCenParaDB
            mMenu = Engine.Common.MenuENG.GetMenuPara(Request("MenuID"))
            If mMenu.ID <> 0 Then
                Response.Redirect(mMenu.MENU_URL & "?ShopID=" & e.CommandArgument & "&MenuID=" & Request("MenuID") & "&rnd=" & DateTime.Now.Millisecond)
            Else
                Session.RemoveAll()
                Me.Response.Redirect("frmLogin.aspx")
            End If
            mMenu = Nothing
        End If
    End Sub

    Private Sub SetShopList()
        Dim uPara As New CenParaDB.Common.UserProfilePara
        uPara = Config.GetLogOnUser
        Dim lLog As New Engine.Common.LoginENG
        Dim dt As New DataTable
        dt = lLog.GetShopListByUser(uPara.USERNAME)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = (i + 1)
            Next

            gvShopList.DataSource = dt
            gvShopList.DataBind()
            dt.Dispose()
        Else
            gvShopList.DataSource = Nothing
            gvShopList.DataBind()
            lblErrorMessage.Visible = True
        End If
        lLog = Nothing
        uPara = Nothing
    End Sub

    Protected Sub gvShopList_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvShopList.RowEditing

    End Sub
End Class
