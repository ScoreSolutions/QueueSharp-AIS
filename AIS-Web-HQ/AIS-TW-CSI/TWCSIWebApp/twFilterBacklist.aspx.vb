Imports System.Data
Imports Engine.Common

Partial Class TWCSIWebApp_twFilterBacklist
    Inherits System.Web.UI.Page

    Const SessFileList As String = "SessFileList"
    Const SessMobileList As String = "SessMobileList"

    'Private Sub SetBacklistFileList(ByVal dt As DataTable)
    '    If dt.Rows.Count > 0 Then
    '        If dt.Columns.Contains("no") = False Then
    '            dt.Columns.Add("no")
    '        End If

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            dt.Rows(i)("no") = (i + 1)
    '        Next

    '        Session(SessFileList) = dt
    '        pcFile.Visible = True
    '        gvBlackListFile.PageSize = 5
    '        pcFile.SetMainGridView(gvBlackListFile)
    '        gvBlackListFile.DataSource = dt
    '        gvBlackListFile.DataBind()
    '        pcFile.Update(dt.Rows.Count)

    '        SearchMobile.Visible = True

    '        For i As Integer = 0 To gvBlackListFile.Rows.Count - 1
    '            gvBlackListFile.Rows(i).Cells(1).Text = (gvBlackListFile.PageIndex * gvBlackListFile.PageSize) + (i + 1)
    '        Next
    '    Else
    '        gvBlackListFile.DataSource = Nothing
    '        gvBlackListFile.DataBind()
    '        Session.Remove(SessFileList)
    '        pcFile.Visible = False
    '        SearchMobile.Visible = False
    '    End If
    'End Sub

    'Protected Sub imgDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim vID As String = sender.CommandArgument

    '    Dim eng As New Engine.CSI.FilterTemplateENG
    '    eng.DeleteFilterBacklistFile(vID)

    '    Dim dt As New DataTable
    '    dt = eng.GetFilterBacklistFileList()
    '    SetBacklistFileList(dt)
    '    dt = Nothing
    '    eng = Nothing
    'End Sub

    'Protected Sub ctlBlackList_Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlBlackList.Upload_Click
    '    If ctlBlackList.HasFile = False Then
    '        Config.SetAlert("Please select Attach File", Me)
    '        Exit Sub
    '    End If

    '    Dim tmpFile As New CenParaDB.Common.TmpFileUploadPara
    '    tmpFile = ctlBlackList.TmpFileUploadPara
    '    If tmpFile.FileExtent.ToUpper.Trim = ".XLS" Or tmpFile.FileExtent.ToUpper.Trim = ".XLSX" Then
    '        Dim eng As New Engine.CSI.FilterTemplateENG
    '        If eng.CheckDupFileName(tmpFile.FileName, 0) = False Then
    '            Dim uPara As New CenParaDB.Common.UserProfilePara
    '            uPara = Config.GetLogOnUser()

    '            If eng.SaveBackListFile(uPara.USERNAME, tmpFile) = True Then
    '                Dim dt As DataTable = eng.GetFilterBacklistFileList()
    '                SetBacklistFileList(dt)
    '                dt = Nothing
    '                ctlBlackList.ClearFile()
    '                Config.SetAlert("Upload Complete", Me)
    '            Else
    '                Config.SetAlert(eng.ErrorMessage, Me)
    '            End If
    '            uPara = Nothing
    '        Else
    '            Config.SetAlert("Duplicate File Name", Me)
    '        End If
    '        eng = Nothing
    '        tmpFile = Nothing
    '    Else
    '        tmpFile = Nothing
    '        Config.SetAlert("Invalid File Type", Me)
    '        Exit Sub
    '    End If
    'End Sub

    'Protected Sub pcFile_PageChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcFile.PageChange
    '    If Session(SessFileList) IsNot Nothing Then
    '        Dim dt As New DataTable
    '        dt = Session(SessFileList)
    '        If dt.Rows.Count > 0 Then
    '            gvBlackListFile.PageIndex = pcFile.SelectPageIndex
    '            SetBacklistFileList(dt)
    '        End If
    '    End If
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Dim eng As New Engine.CSI.FilterTemplateENG
            'Dim dt As New DataTable
            'dt = eng.GetFilterBacklistFileList()
            'SetBacklistFileList(dt)
            'dt = Nothing
            'eng = Nothing

            'txtSearchFileName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearchMobile.ClientID + "') ")
            txtSearchMobile.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearchMobile.ClientID + "') ")
            txtSearchFileName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearchMobile.ClientID + "') ")
        End If
    End Sub

    Protected Sub btnSearchMobile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchMobile.Click
        Dim wh As String = ""
        If txtSearchMobile.Text.Trim <> "" Then
            wh += " and bm.mobile_no like '%" & txtSearchMobile.Text.Trim & "%'"
        End If
        If txtSearchFileName.Text.Trim <> "" Then
            wh += " and bf.file_name like '%" & txtSearchFileName.Text.Trim & "%'"
        End If

        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim dt As New DataTable
        dt = eng.GetMobileList(wh)
        gvMobileList.PageIndex = 0
        Session.Remove("IsDup")
        SetMobileList(dt)
        If dt.Rows.Count = 0 Then
            Config.SetAlert("No data found", Me)
        End If
        dt = Nothing
        eng = Nothing
    End Sub

    'Protected Sub btnDuplicate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDuplicate.Click
    '    Dim eng As New Engine.CSI.FilterTemplateENG
    '    Dim dt As New DataTable
    '    dt = eng.GetDuplicateMobileList()
    '    gvMobileList.PageIndex = 0
    '    SetMobileList(dt)

    '    Session("IsDup") = "Y"
    '    If dt.Rows.Count = 0 Then
    '        Config.SetAlert("No data found", Me)
    '    End If
    '    dt = Nothing
    '    eng = Nothing
    'End Sub

    Private Sub SetMobileList(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            If dt.Columns.Contains("no") = False Then
                dt.Columns.Add("no")
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = (i + 1)
            Next

            Session(SessMobileList) = dt
            pcMobile.Visible = True
            gvMobileList.PageSize = 50
            pcMobile.SetMainGridView(gvMobileList)
            gvMobileList.DataSource = dt
            gvMobileList.DataBind()
            pcMobile.Update(dt.Rows.Count)

            For i As Integer = 0 To gvMobileList.Rows.Count - 1
                gvMobileList.Rows(i).Cells(1).Text = (gvMobileList.PageIndex * gvMobileList.PageSize) + (i + 1)
            Next
        Else
            gvMobileList.DataSource = Nothing
            gvMobileList.DataBind()
            Session.Remove(SessMobileList)
            pcMobile.Visible = False
        End If
    End Sub
    Protected Sub imgDeleteMobile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim vID As String = sender.CommandArgument

        Dim eng As New Engine.CSI.FilterTemplateENG
        eng.DeleteBacklistMobile(vID)
        eng = Nothing

        btnSearchMobile_Click(Nothing, Nothing)
    End Sub

    Protected Sub pcMobile_PageChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcMobile.PageChange
        If Session(SessMobileList) IsNot Nothing Then
            Dim dt As New DataTable
            dt = Session(SessMobileList)
            gvMobileList.PageIndex = pcMobile.SelectPageIndex
            SetMobileList(dt)
            dt = Nothing
        End If
    End Sub

    'Protected Sub gvBlackListFile_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvBlackListFile.Sorted
    '    For i As Integer = 0 To gvBlackListFile.Rows.Count - 1
    '        gvBlackListFile.Rows(i).Cells(1).Text = (gvBlackListFile.PageIndex * gvBlackListFile.PageSize) + (i + 1)
    '    Next
    'End Sub

    'Protected Sub gvBlackListFile_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvBlackListFile.Sorting
    '    Dim dt As New DataTable
    '    dt = Session(SessFileList)
    '    If dt.Rows.Count > 0 Then
    '        Config.GridViewSorting(gvBlackListFile, dt, txtFileSortDir, txtFileSortField, e, pcFile.SelectPageIndex)
    '        SetBacklistFileList(dt)
    '    End If
    '    dt = Nothing
    'End Sub

    Protected Sub gvMobileList_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvMobileList.Sorted
        For i As Integer = 0 To gvMobileList.Rows.Count - 1
            gvMobileList.Rows(i).Cells(1).Text = (gvMobileList.PageIndex * gvMobileList.PageSize) + (i + 1)
        Next
    End Sub

    Protected Sub gvMobileList_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvMobileList.Sorting
        Dim dt As New DataTable
        dt = Session(SessMobileList)
        If dt.Rows.Count > 0 Then
            Config.GridViewSorting(gvMobileList, dt, txtMobileSortDir, txtMobileSortField, e, pcMobile.SelectPageIndex)
            SetMobileList(dt)
        End If
        dt = Nothing
    End Sub


End Class
