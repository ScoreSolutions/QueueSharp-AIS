Imports Engine.Configuration
Imports System.Data
Imports System.Drawing
Partial Class frmMSTHoldReason
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            GetHoldReasonList()
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_reason.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            Config.SaveTransLog("คลิกเมนู : Central Setup >> Hold Reason", "AisWebConfig.frmMSTHoldReason.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub GetHoldReasonList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetHoldReasonList("1=1")
        If dt.Rows.Count > 0 Then
            dgvHoldReasonList.DataSource = dt
            dgvHoldReasonList.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbHoldReasonCenParaDB
            With p
                .ID = txt_id.Text
                .NAME = txt_Reason.Text.Trim
                If chk_productive.Checked = True Then
                    .PRODUCTIVE = "1"
                Else
                    .PRODUCTIVE = "0"
                End If
                If chk_active.Checked = True Then
                    .ACTIVE_STATUS = "1"
                Else
                    .ACTIVE_STATUS = "0"
                End If
            End With

            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            Dim eng As New Engine.Configuration.MasterENG
            If eng.SaveHoldReason(uPara.USERNAME, p) = True Then

                Dim engSh As New Engine.Configuration.ShopHoldReasonENG
                engSh.SaveShHoldReason(uPara.USERNAME, p)

                GetHoldReasonList()
                Config.ShowAlert("Save Complete.", Me)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If

            eng = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Central Setup >> Hold Reason", "AisWebConfig.frmMSTHoldReason.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If
    End Sub


    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txt_reason.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Reason", Me, txt_reason.ClientID)
       
        Else
            Dim eng As New MasterENG
            If eng.CheckDuplicateHoldReason(txt_id.Text, txt_reason.Text) = True Then
                Config.ShowAlert("Hold Reason is duplicate", Me, txt_reason.ClientID)
                ret = False
            End If
        End If

        Return ret
    End Function

    Private Sub ClearData()
        txt_id.Text = "0"
        txt_reason.Text = ""
        chk_productive.Checked = False
        chk_active.Checked = True

        lblNotFound.Visible = False
        dgvHoldReasonList.Visible = True
        txt_search.Text = String.Empty
        SearchData()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Central Setup >> Hold Reason", "AisWebConfig.frmMSTHoldReason.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Central Setup >> Hold Reason", "AisWebConfig.frmMSTHoldReason.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Sub SearchData()
        dgvHoldReasonList.CurrentPageIndex = 0
        Dim eng As New MasterENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " Name like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.GetHoldReasonList(wh)

        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvHoldReasonList.Visible = False
        Else
            lblNotFound.Visible = False
            dgvHoldReasonList.Visible = True
            dgvHoldReasonList.DataSource = dt
            dgvHoldReasonList.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub dgvHoldReasonList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvHoldReasonList.PageIndexChanged
        dgvHoldReasonList.CurrentPageIndex = e.NewPageIndex
        GetHoldReasonList()
    End Sub

    Protected Sub dgvHoldReasonList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvHoldReasonList.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim p As New CenParaDB.TABLE.TbHoldReasonCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetHoldReasonPara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_reason.Text = p.NAME
                chk_productive.Checked = IIf(p.PRODUCTIVE = "1", True, False)
                chk_active.Checked = IIf(p.ACTIVE_STATUS = "1", True, False)
            End If

            'GetHoldReasonList()

        End If
    End Sub

    Function SetProductive(ByVal Productive As String) As String
        If Productive = "1" Then
            Return "Productive"
        Else
            Return "Non Productive"
        End If
    End Function
End Class
