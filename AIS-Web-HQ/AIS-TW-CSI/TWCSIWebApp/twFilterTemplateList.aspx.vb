Imports System.Data
Imports Engine.CSI

Partial Class TWCSIWebApp_twFilterTemplateList
    Inherits System.Web.UI.Page

    Const CelID As Integer = 7

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Response.Redirect("../TWCSIWebApp/twFilterTemplateForm.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim wh As String = ""
            If rdiStatus.SelectedValue <> "0" Then
                wh = " f.active_status = '" & rdiStatus.SelectedValue & "'"
            Else
                wh = "1=1"
            End If
            SetFilterList(wh)

            txtSearchFilterName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "') ")
            txtSearchTemplateCode.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "') ")
        End If
    End Sub

    Private Sub SetFilterList(ByVal wh As String)
        Session.Remove("SearchFilterList")
        Dim eng As New FilterTemplateENG
        Dim dt As DataTable = eng.GetTWFilterList(wh)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("no")
            dt.Columns.Add("period_date")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = i + 1

                If Convert.IsDBNull(dt.Rows(i)("preiod_datefrom")) = False Then
                    dt.Rows(i)("period_date") = Convert.ToDateTime(dt.Rows(i)("preiod_datefrom")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                End If
                dt.Rows(i)("period_date") += " - "

                If Convert.IsDBNull(dt.Rows(i)("preiod_dateto")) = False Then
                    dt.Rows(i)("period_date") += Convert.ToDateTime(dt.Rows(i)("preiod_dateto")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                End If
            Next

            PageControl1.SetMainGridView(gvFilterList)
            gvFilterList.PageSize = 50
            gvFilterList.DataSource = dt
            gvFilterList.DataBind()
            PageControl1.Update(dt.Rows.Count)
            Session("SearchFilterList") = dt
            dt.Dispose()
        Else
            PageControl1.Visible = False
            gvFilterList.DataSource = Nothing
            gvFilterList.DataBind()
            Session.Remove("SearchFilterList")
        End If
        eng = Nothing
    End Sub

    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Response.Redirect("../TWCSIWebApp/twFilterTemplateForm.aspx?id=" & grv.Cells(CelID).Text & "&rnd=" & DateTime.Now.Millisecond)
    End Sub

    Protected Sub imgCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Response.Redirect("../TWCSIWebApp/twFilterTemplateForm.aspx?id=" & grv.Cells(CelID).Text & "&cmd=copy&rnd=" & DateTime.Now.Millisecond)
    End Sub

    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Dim eng As New FilterTemplateENG
        If eng.DeleteTWFilterTemplate(grv.Cells(CelID).Text) = True Then
            btnSearch_Click(Nothing, Nothing)
        End If
        eng = Nothing
    End Sub

    Protected Sub imgVerify_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Response.Redirect("../TWCSIWebApp/twVerifyData.aspx?id=" & grv.Cells(CelID).Text & "&rnd=" & DateTime.Now.Millisecond)
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim wh As String = "1=1"
        If txtSearchFilterName.Text.Trim <> "" Then
            wh += " and f.filter_name like '%" & txtSearchFilterName.Text.Trim & "%'"
        End If
        If txtSearchTemplateCode.Text.Trim <> "" Then
            wh += " and f.template_code like '%" & txtSearchTemplateCode.Text.Trim & "%'"
        End If
        If rdiStatus.SelectedValue <> "0" Then
            wh += " and f.active_status='" & rdiStatus.SelectedValue & "'"
        End If

        SetFilterList(wh)
    End Sub

    Protected Sub PageControl1_PageChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles PageControl1.PageChange
        Dim dt As New DataTable
        dt = Session("SearchFilterList")
        If dt.Rows.Count > 0 Then
            gvFilterList.PageIndex = PageControl1.SelectPageIndex
            PageControl1.SetMainGridView(gvFilterList)
            gvFilterList.DataSource = dt
            gvFilterList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
    End Sub

    Protected Sub gvFilterList_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvFilterList.Sorted
        For i As Integer = 0 To gvFilterList.Rows.Count - 1
            gvFilterList.Rows(i).Cells(0).Text = (gvFilterList.PageIndex * gvFilterList.PageSize) + (i + 1)
        Next
    End Sub

    Protected Sub gvFilterList_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFilterList.Sorting
        Dim dt As New DataTable
        dt = Session("SearchFilterList")
        If dt.Rows.Count > 0 Then
            gvFilterList.PageIndex = PageControl1.SelectPageIndex
            Config.GridViewSorting(gvFilterList, dt, txtSortDir, txtSortField, e, PageControl1.SelectPageIndex)
            PageControl1.SetMainGridView(gvFilterList)
            gvFilterList.DataSource = dt
            gvFilterList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
        dt = Nothing
    End Sub
End Class
