Imports System.Data
Imports Engine.Common
Partial Class CSIWebApp_frmActivity
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'SetCategoryList(" 1=1 ")
            PageControl1.Visible = False
            txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
        End If
    End Sub

    Sub SetCategoryList(ByVal wh As String)
        Dim eng As New Engine.CSI.CSIMasterENG
        Dim dt As New DataTable
        dt = eng.GetActivityListAll(wh, "AIS")
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = i + 1
            Next

            PageControl1.SetMainGridView(gvCategoryList)
            gvCategoryList.PageSize = 20
            gvCategoryList.DataSource = dt
            gvCategoryList.DataBind()
            PageControl1.Update(dt.Rows.Count)
            Session("SearchFilterCategoryList") = dt
            dt.Dispose()
            lblNotFound.Visible = False
        Else
            gvCategoryList.DataSource = Nothing
            gvCategoryList.DataBind()
            PageControl1.Visible = False
            Session("SearchFilterCategoryList") = Nothing
            lblNotFound.Visible = True
        End If

    End Sub

    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Dim eng As New Engine.CSI.CSIMasterENG
        If eng.DeleteCategory(grv.Cells(1).Text, Replace(grv.Cells(2).Text, "&nbsp;", ""), "AIS") = True Then
            SetCategoryList(" 1=1 ")
        End If
        eng = Nothing
    End Sub

    Protected Sub PageControl1_PageChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles PageControl1.PageChange
        Dim dt As New DataTable
        dt = Session("SearchFilterCategoryList")
        If dt.Rows.Count > 0 Then
            gvCategoryList.PageIndex = PageControl1.SelectPageIndex
            PageControl1.SetMainGridView(gvCategoryList)
            gvCategoryList.DataSource = dt
            gvCategoryList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
    End Sub

    Protected Sub gvCategoryList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCategoryList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim imgdel As New ImageButton
            Dim gvrow As GridViewRow = e.Row
            imgdel = gvrow.FindControl("imgDelete")

            Dim category As String = gvrow.Cells(1).Text
            Dim subcategory As String = Replace(gvrow.Cells(2).Text, "&nbsp;", "")

            If subcategory <> "" Then
                If IsRefSubCategory(category, subcategory) Then
                    imgdel.Visible = False
                End If
            Else
                If IsRefCategory(category) Then
                    imgdel.Visible = False
                End If
            End If

        End If
    End Sub

    Protected Sub gvCategoryList_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCategoryList.Sorting
        Dim dt As New DataTable
        dt = Session("SearchFilterList")
        If dt.Rows.Count > 0 Then
            gvCategoryList.PageIndex = PageControl1.SelectPageIndex
            Config.GridViewSorting(gvCategoryList, dt, txtSortDir, txtSortField, e, PageControl1.SelectPageIndex)
            PageControl1.SetMainGridView(gvCategoryList)
            gvCategoryList.DataSource = dt
            gvCategoryList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
        dt = Nothing
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim filter As String = "1=1"
        If Trim(txtSearch.Text) <> "" Then
            filter = " (replace(replace(replace(sac.category_name,'_','9x9x9'),'[','9x9x9'),']','9x9x9') like replace(replace(replace('%" & Trim(txtSearch.Text) & "%','_','9x9x9'),'[','9x9x9'),']','9x9x9')"
            filter &= " or  replace(replace(replace(subcategory_name,'_','9x9x9'),'[','9x9x9'),']','9x9x9') like replace(replace(replace('%" & Trim(txtSearch.Text) & "%','_','9x9x9'),'[','9x9x9'),']','9x9x9'))"
        End If
        SetCategoryList(filter)
    End Sub


    Protected Sub ctlActivityList_Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlActivityList.Upload
        If ctlActivityList.HasFile = False Then
            Config.SetAlert("Please select File", Me)
            Exit Sub
        End If

        Dim tmpFile As New CenParaDB.Common.TmpFileUploadPara
        tmpFile = ctlActivityList.TmpFileUploadPara

        Dim eng As New Engine.CSI.CSIMasterENG
        Dim uPara As New CenParaDB.Common.UserProfilePara
        uPara = Config.GetLogOnUser()

        If eng.SaveActivityFromFile(uPara.USERNAME, tmpFile, "AIS") = True Then
            ctlActivityList.ClearFile()
            Config.SetAlert("บันทึกข้อมูลเรียบร้อย", Me)
            SetCategoryList(" 1=1 ")
        Else
            Config.SetAlert(eng.ErrorMessage, Me)
        End If

        ctlActivityList.ClearFile()
        tmpFile = Nothing
        eng = Nothing

    End Sub

    Function IsRefSubCategory(ByVal category As String, ByVal subcategory As String) As Boolean
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnqFb As New CenLinqDB.TABLE.TbFilterCenLinqDB
        Dim dt As New DataTable
        dt = lnqFb.GetDataList(" (category_gen_act_all_survey='" & category & "' and subcategory_gen_act_all_survey ='" & subcategory & "')" & _
                               "  or (category_gen_act_result3 ='" & category & "' and subcategory_gen_act_result3 ='" & subcategory & "')" & _
                               "  or (cat_gen_act_all_survey_upc ='" & category & "' and subcat_gen_act_all_survey_upc ='" & subcategory & "')" & _
                               "  or (cat_gen_act_result3_upc ='" & category & "' and subcat_gen_act_result3_upc ='" & subcategory & "')", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Return True
        End If

        trans.CommitTransaction()
        lnqFb = Nothing

        Return False
    End Function

    Function IsRefCategory(ByVal category As String) As Boolean
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnqFb As New CenLinqDB.TABLE.TbFilterCenLinqDB
        Dim dt As New DataTable
        dt = lnqFb.GetDataList("category_gen_act_all_survey='" & category & _
                               "' or category_gen_act_result3 ='" & category & _
                               "' or cat_gen_act_all_survey_upc ='" & category & _
                               "' or cat_gen_act_result3_upc ='" & category & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Return True
        End If

        trans.CommitTransaction()
        lnqFb = Nothing

        Return False
    End Function
End Class
