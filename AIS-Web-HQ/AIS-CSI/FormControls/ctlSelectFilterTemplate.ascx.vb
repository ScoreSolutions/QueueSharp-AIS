Imports System.Data
Imports Engine.CSI

Partial Class FormControls_ctlSelectFilterTemplate
    Inherits System.Web.UI.UserControl

    Const SessSelectFilter As String = "SessSelectFilter"
    'Private _DateFrom As String
    'Private _DateTo As String
    Public Event Search(ByVal sender As Object, ByVal e As System.EventArgs)
    Public WriteOnly Property DateFrom() As String
        Set(ByVal value As String)
            lblSearchDateFrom.Text = value
        End Set
    End Property

    Public WriteOnly Property DateTo() As String
        Set(ByVal value As String)
            lblSearchDateTo.Text = value
        End Set
    End Property

    Protected Sub chkSearchFilterList_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
        zPop.Show()
    End Sub
    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent

        Dim dt As New DataTable
        dt = Session(SessSelectFilter)
        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            If dt.Rows(i)("id").ToString = grv.Cells(2).Text Then
                dt.Rows.RemoveAt(i)
            End If
        Next

        SetSelectFilterList(dt)
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblSearchDateFrom.Text = ""
        lblSearchDateTo.Text = ""
        RaiseEvent Search(sender, e)
        If lblSearchDateFrom.Text.Trim = "" Or lblSearchDateTo.Text.Trim = "" Then
            Exit Sub
        End If

        gvSearchTemplateList.DataSource = Nothing
        gvSearchTemplateList.DataBind()
        zPop.Show()
    End Sub

    Public Function GetSelectedFilterID() As String
        Dim ret As String = ""
        For Each grv As GridViewRow In gvSelectFilter.Rows
            If ret = "" Then
                ret = grv.Cells(2).Text
            Else
                ret += "," & grv.Cells(2).Text
            End If
        Next
        Return ret
    End Function

    Public Sub ClearSelectFilterID()
        gvSelectFilter.DataSource = Nothing
        gvSelectFilter.DataBind()
        Session.Remove(SessSelectFilter)
    End Sub

    Protected Sub btnPopSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopSearch.Click
        Dim wh As String = "1=1"

        If lblSearchDateFrom.Text.Trim <> "" And lblSearchDateTo.Text.Trim <> "" Then
            wh += " and (convert(varchar(8),preiod_datefrom,112) between '" & lblSearchDateFrom.Text & "' and '" & lblSearchDateTo.Text & "'"
            wh += " or convert(varchar(8),preiod_dateto,112) between '" & lblSearchDateFrom.Text & "' and '" & lblSearchDateTo.Text & "'"

            wh += " or '" & lblSearchDateFrom.Text & "' between convert(varchar(8),preiod_datefrom,112) and convert(varchar(8),preiod_dateto,112) "
            wh += " or '" & lblSearchDateTo.Text & "' between convert(varchar(8),preiod_datefrom,112) and convert(varchar(8),preiod_dateto,112))"
        End If

        If txtSearchFilterName.Text.Trim <> "" Then
            wh += " and f.filter_name like '%" & txtSearchFilterName.Text.Trim & "%'"
        End If

        Dim SelectFilterID As String = GetSelectedFilterID()
        If SelectFilterID.Trim <> "" Then
            wh += " and f.id not in (" & SelectFilterID & ")"
        End If

        Dim dt As New DataTable
        Dim eng As New FilterTemplateENG
        dt = eng.GetFilterList(wh)
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
            gvSearchTemplateList.DataSource = dt
            gvSearchTemplateList.DataBind()
            dt = Nothing
            lblNoDataFound.Visible = False
        Else
            gvSearchTemplateList.DataSource = Nothing
            gvSearchTemplateList.DataBind()
            lblNoDataFound.Visible = True
        End If
        eng = Nothing

        zPop.Show()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session.Remove(SessSelectFilter)
            txtSearchFilterName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnPopSearch.ClientID + "') ")
        End If
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim dt As New DataTable
        If Session(SessSelectFilter) IsNot Nothing Then
            dt = Session(SessSelectFilter)
        Else
            dt.Columns.Add("id")
            dt.Columns.Add("filter_name")
        End If

        For Each grv As GridViewRow In gvSearchTemplateList.Rows
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                Dim dr As DataRow = dt.NewRow
                dr("id") = grv.Cells(6).Text
                dr("filter_name") = grv.Cells(2).Text
                dt.Rows.Add(dr)
            End If
        Next

        SetSelectFilterList(dt)
    End Sub

    Private Sub SetSelectFilterList(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            gvSelectFilter.DataSource = dt
            gvSelectFilter.DataBind()
            Session(SessSelectFilter) = dt
        Else
            gvSelectFilter.DataSource = Nothing
            gvSelectFilter.DataBind()
            Session.Remove(SessSelectFilter)
            zPop.Show()
        End If
    End Sub
End Class
