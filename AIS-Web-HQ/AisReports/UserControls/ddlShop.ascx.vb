Imports System.Data
Imports Engine
Partial Class UserControls_ddlShop
    Inherits System.Web.UI.UserControl
    Private _shop As String
    Private _region As String

    Public Property Shop() As String
        Get
            Return ddlShop.SelectedItem.Text
        End Get
        Set(ByVal value As String)
            _shop = value
        End Set
    End Property

    Public WriteOnly Property Region() As String
        Set(ByVal value As String)
            _region = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetItemList()
        End If
    End Sub

    Public Sub SetItemList()
        ddlShop.Items.Clear()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        Dim strRegionFilter As String = "  region_id <> 1"
        If _region = "BKK" Then
            strRegionFilter = "  region_id = 1"
        ElseIf _region = "" Then
            strRegionFilter = "1=1"
        End If
        dt = eng.GetShopList("" & strRegionFilter & "")
        ddlShop.Items.Add(New ListItem("All", ""))
        For i As Integer = 0 To dt.Rows.Count - 1
            ddlShop.Items.Add(New ListItem(dt.Rows(i).Item("shop_name_en") & "", dt.Rows(i).Item("shop_code") & ""))
        Next
    End Sub
End Class
