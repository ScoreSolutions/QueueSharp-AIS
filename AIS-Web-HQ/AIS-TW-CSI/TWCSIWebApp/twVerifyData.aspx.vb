Imports Engine.CSI
Imports System.Data

Partial Class TWCSIWebApp_twVerifyData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request("id") IsNot Nothing Then
                FillData(Request("id"))
            End If
        End If
    End Sub

    Private Sub FillData(ByVal vID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim para As New CenParaDB.TABLE.TwFilterCenParaDB
        para = eng.GetTWFilterTemplatePara(vID, trans) ' .GetFilterTemplatePara(vID, trans)
        trans.CommitTransaction()
        txtID.Text = para.ID
        lblTemplateName.Text = para.FILTER_NAME
        If para.TARGET_UNLIMITED = "Y" Then
            lblTarget.Text = "Target : Unlimited"
        Else
            lblTarget.Text = "Target " & para.TARGET
        End If
        para = Nothing
        eng = Nothing
    End Sub

    Private Sub SetTWDataList(ByVal vID As Long, ByVal FilterLocationDT As DataTable, ByVal SearchDate As String)
        Dim eng As New VerifyDataENG

        If FilterLocationDT.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' class='mGrid' >")
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td width='20px' >&nbsp;</td>")
            ret.Append("        <td align='center' >Order Type</td>")
            ret.Append("        <td width='50px' align='center' >Regis</td>")
            ret.Append("        <td width='50px' align='center' >% Target</td>")
            ret.Append("        <td width='50px' align='center' >No. Target</td>")
            ret.Append("        <td width='50px' align='center' >Data For Send</td>")
            ret.Append("        <td width='50px' align='center' >Send Complete</td>")
            ret.Append("        <td width='50px' align='center' >Return Result</td>")
            ret.Append("        <td width='50px' align='center' >Status</td>")
            ret.Append("        <td width='150px' align='center' >Detail</td>")
            ret.Append("    </tr>")


            'GetData All Location by Template
            Dim dt As New DataTable
            dt = eng.GetTWVerifyDataList(vID, SearchDate)
            For i As Integer = 0 To FilterLocationDT.Rows.Count - 1
                Dim Sdr As DataRow = FilterLocationDT.Rows(i)

                Dim brPara As New CenParaDB.TABLE.TwLocationCenParaDB
                Dim brEng As New Engine.Configuration.MasterENG
                brPara = brEng.GetTWLocationPara(Sdr("tw_location_id"))
                ret.Append("    <tr style='background: #B7D575 repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & (i + 1) & "</td>")
                ret.Append("        <td align='left' colspan='9' >" & brPara.LOCATION_NAME_TH & " (" & brPara.LOCATION_CODE & ")</td>")
                ret.Append("    </tr>")

                dt.DefaultView.RowFilter = "location_id='" & brPara.ID & "'"
                If dt.DefaultView.Count > 0 Then
                    Dim totRegis As Long = 0
                    Dim totTargetPer As Long = 0
                    Dim totNoTarget As Long = 0
                    Dim totDataForSend As Long = 0
                    Dim totSendComplete As Long = 0
                    Dim totReturnResult As Long = 0

                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr style='background-color:white' >")
                        ret.Append("        <td >&nbsp;</td>")
                        ret.Append("        <td align='left' >&nbsp;" & dr("order_type_name") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & FormatNumber(Val(dr("regis") & ""), 0) & "</td>")
                        If dr("target_unlimited").ToString = "Y" Then
                            ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                            ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                        Else
                            ret.Append("        <td align='center' >&nbsp;" & dr("target_percent") & " %</td>")
                            ret.Append("        <td align='center' >&nbsp;" & Math.Ceiling(Convert.ToDouble(Val(dr("notarget") & ""))) & "</td>")
                        End If
                        ret.Append("        <td align='center' >&nbsp;" & dr("data_for_send") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("send_complete") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("return_result") & "</td>")

                        Dim vStatus As String = ""
                        If Convert.ToDouble(dr("return_result")) <> Math.Ceiling(Convert.ToDouble(dr("send_complete"))) Then
                            vStatus = "Sending"
                        Else
                            vStatus = "Send Complete"
                        End If

                        If Convert.ToDouble(dr("return_result")) = 0 Then
                            vStatus = "Sending"
                        End If

                        ret.Append("        <td align='center' >&nbsp;" & vStatus & "</td>")
                        ret.Append("        <td align='center' >")
                        ret.Append("            <input type='button' class='formDialog' value=' Detail ' onClick='OpenResultWindow(" & vID & ", " & Sdr("tw_location_id") & "," & dr("order_type_id") & "," & SearchDate & ")' />")
                        ret.Append("        </td>")
                        ret.Append("    </tr>")
                        totRegis += Convert.ToInt64(Val(dr("Regis") & ""))
                        totTargetPer += Convert.ToInt64(dr("target_percent"))
                        totNoTarget += Math.Ceiling(Convert.ToDouble(Val(dr("notarget") & "")))
                        totDataForSend += Convert.ToInt64(dr("data_for_send"))
                        totSendComplete += Convert.ToInt64(dr("send_complete"))
                        totReturnResult += Convert.ToInt64(dr("return_result"))
                    Next
                    'Total Row By Shop
                    ret.Append("    <tr style='background-color:gray' >")
                    ret.Append("        <td align='center' colspan='2' >&nbsp;Total</td>")
                    ret.Append("        <td align='center' >&nbsp;" & FormatNumber(totRegis, 0) & "</td>")
                    If dt.Rows(0)("target_unlimited").ToString = "Y" Then
                        ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                        ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                    Else
                        ret.Append("        <td align='center' >&nbsp;" & totTargetPer & " %</td>")
                        ret.Append("        <td align='center' >&nbsp;" & totNoTarget & "</td>")
                    End If
                    ret.Append("        <td align='center' >&nbsp;" & totDataForSend & "</td>")
                    ret.Append("        <td align='center' >&nbsp;" & totSendComplete & "</td>")
                    ret.Append("        <td align='center' >&nbsp;" & totReturnResult & "</td>")
                    ret.Append("        <td align='center' colspan='2' >&nbsp;</td>")
                    ret.Append("    </tr>")
                End If
                dt.DefaultView.RowFilter = ""
            Next
            ret.Append("</table>")
            dt.Dispose()

            lblDesc.Text = ret.ToString
        Else
            lblDesc.Text = ""
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

        'Dim eng As New Engine.CSI.FilterTemplateENG
        'Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
        'para = eng.GetFilterTemplatePara(txtID.Text, trans)
        'trans.CommitTransaction()
        'txtID.Text = para.ID

        'SetDataList(para.ID, para.CHILD_TB_FILTER_SHOP_tb_filter_id, txtSearchDate.GetDateCondition)
        If txtSearchDate.DateValue.Year = 1 Then
            Config.SetAlert("กรุณาเลือกวันที่", Me)
            Exit Sub
        End If
        searchdata()
        'Response.Redirect("../TWCSIWebApp/twVerifyData.aspx?rnd=" & Rnd() & "&id=" & txtID.Text & "&vDate=" & txtSearchDate.GetDateCondition & "&LocationCode=" & txtSearchLocationCode.Text.Trim)
    End Sub

    Sub searchdata()
        If txtSearchDate.GetDateCondition IsNot Nothing And txtSearchLocationCode.Text.Trim IsNot Nothing Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            Dim eng As New Engine.CSI.FilterTemplateENG
            Dim para As New CenParaDB.TABLE.TwFilterCenParaDB
            para = eng.GetTWFilterTemplatePara(txtID.Text, trans)


            Dim dttemp As New DataTable
            Dim lnqtemp As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
            Dim sql As String = "select tw_filter_data.id from tw_filter_data inner join tw_location  on tw_location.id = tw_filter_data.tw_location_id "
            sql += " where convert(varchar(8), service_date,112) ='" & txtSearchDate.GetDateCondition & "'"
            If txtSearchLocationCode.Text.Trim <> "" Then
                sql += " and location_code ='" & txtSearchLocationCode.Text.Trim & "'"
            End If
            sql += " and tw_filter_id ='" & txtID.Text & "'"

            dttemp = lnqtemp.GetListBySql(sql, trans.Trans)
            If dttemp.Rows.Count = 0 Then
                lblNotFound.Visible = True
                lblDesc.Text = ""
                Exit Sub
            Else
                lblNotFound.Visible = False
            End If

            Dim dt As New DataTable
            Dim lnq As New CenLinqDB.TABLE.TwFilterBranchCenLinqDB
            sql = "select fb.*"
            sql += " from tw_filter_branch fb"
            sql += " inner join tw_location l on l.id=fb.tw_location_id"
            sql += " where fb.tw_filter_id='" & txtID.Text & "' and l.location_code like '%" & txtSearchLocationCode.Text.Trim & "%'"

            dt = lnq.GetListBySql(sql, trans.Trans) 'lnq.GetDataList("tw_filter_id = '" & txtID.Text & "'", "", trans.Trans)
            If dt.Rows.Count = 0 Then
                lblNotFound.Visible = True
                lblDesc.Text = ""
            Else
                lblNotFound.Visible = False
            End If


            SetTWDataList(txtID.Text, dt, txtSearchDate.GetDateCondition)
            trans.CommitTransaction()
            dt.Dispose()
            lnq = Nothing
        End If
    End Sub
End Class
