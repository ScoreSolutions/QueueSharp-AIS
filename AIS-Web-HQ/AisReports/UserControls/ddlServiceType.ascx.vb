Imports System.Data
Imports Engine

Partial Class UserControls_ddlServiceType
    Inherits System.Web.UI.UserControl
    Private _service_type As String
    Dim _DataValueField As String = ""

    Public WriteOnly Property DataValueField() As String
        Set(ByVal value As String)
            _DataValueField = value
        End Set
    End Property

    Public WriteOnly Property Width() As System.Web.UI.WebControls.Unit
        Set(ByVal value As System.Web.UI.WebControls.Unit)
            ddlServiceType.Width = value
        End Set
    End Property

    Public Property ServiceType() As String
        Get
            Return ddlServiceType.SelectedValue
        End Get
        Set(ByVal value As String)
            _service_type = value
        End Set
    End Property

    Public Property ServiceTypeName() As String
        Get
            Return ddlServiceType.SelectedItem.Text
        End Get
        Set(ByVal value As String)
            _service_type = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If _DataValueField = "" Then
                SetItemList()
            Else
                SetItemList(_DataValueField)
            End If
        End If
    End Sub

    Public Sub SetItemList(Optional ByVal DataValueField As String = "item_code")
        ddlServiceType.Items.Clear()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetServiceAllList(" active_status = 1")
        If _DataValueField.Trim = "id" Then
            ddlServiceType.Items.Add(New ListItem("All", "0"))
        Else
            ddlServiceType.Items.Add(New ListItem("All", ""))
        End If

        For i As Integer = 0 To dt.Rows.Count - 1
            ddlServiceType.Items.Add(New ListItem(dt.Rows(i).Item("item_name") & "", dt.Rows(i).Item(DataValueField) & ""))
        Next
        dt.Dispose()
    End Sub

End Class
