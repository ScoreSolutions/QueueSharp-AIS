Imports Engine.Common
Imports System.Data
Imports Engine.CSI

Partial Class TWCSIWebApp_twSetFilterToShop
    Inherits System.Web.UI.Page

    Protected Sub chkH_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
            SetShopList()
        End If
    End Sub

    Private Sub SetCombo()
        Dim eng As New FilterTemplateENG
        cmbFilterName.SetItemList(eng.GetFilterList("active_status = 'Y'"), "filter_name", "id")
    End Sub
    Private Sub SetShopList()
        Dim eng As New FilterTemplateENG
        Dim dt As DataTable = eng.GetShopList()
        If dt.Rows.Count > 0 Then
            gvShopList.DataSource = dt
            gvShopList.DataBind()
        Else
            gvShopList.DataSource = Nothing
            gvShopList.DataBind()
        End If
    End Sub

    Private Function GetSelectedShop() As String
        Dim ret As String = ""
        For Each gv As GridViewRow In gvShopList.Rows
            Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
            If chk.Checked Then
                If ret = "" Then
                    ret = gv.Cells(1).Text
                Else
                    ret += "," & gv.Cells(1).Text
                End If
            End If
        Next

        Return ret
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If Valid() = True Then
        '    Dim trans As New CenDbTrans
        '    If trans.Trans IsNot Nothing Then
        '        Dim eng As New FilterTemplateENG
        '        If eng.SaveSetFilterToShop(Config.GetLogOnUser().USERNAME, cmbFilterName.SelectedValue, GetSelectedShop(), trans) Then
        '            trans.CommitTransaction()
        '            Config.SetAlert("บันทึกข้อมูลเรียบร้อย", Me)
        '        Else
        '            trans.RollbackTransaction()
        '            Config.SetAlert(eng.ErrorMessage, Me)
        '        End If
        '    End If
        'End If
    End Sub

    Private Function Valid() As Boolean
        Dim ret As Boolean = True
        If cmbFilterName.SelectedValue = "0" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Filter Template", Me, cmbFilterName.ClientID)
        ElseIf GetSelectedShop().Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Shop", Me)
        End If

        Return ret
    End Function

    Protected Sub cmbFilterName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFilterName.SelectedIndexChanged
        SetShopList()
        If cmbFilterName.SelectedValue <> "0" Then
            Dim eng As New FilterTemplateENG
            Dim dt As DataTable = eng.GetFilterShopList(cmbFilterName.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each gv As GridViewRow In gvShopList.Rows
                    dt.DefaultView.RowFilter = "tb_shop_id = '" & gv.Cells(1).Text & "'"
                    If dt.DefaultView.Count > 0 Then
                        Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
                        chk.Checked = True
                    End If
                Next
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        cmbFilterName.SelectedValue = "0"
        SetShopList()
    End Sub
End Class
