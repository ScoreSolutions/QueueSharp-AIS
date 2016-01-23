Imports Engine.CSI
Imports System.Data

Partial Class CSIWebApp_frmVerifyData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request("id") IsNot Nothing Then
                FillData(Request("id"))

                If Request("vDate") IsNot Nothing And Request("LocationCode") IsNot Nothing Then
                    If Not Request("strDate") Is Nothing Then txtSearchDate.TxtBox.Text = Request("strDate")
                    txtSearchLocationCode.Text = Request("LocationCode")

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Dim eng As New Engine.CSI.FilterTemplateENG
                    Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
                    para = eng.GetFilterTemplatePara(txtID.Text, trans)

                    Dim dttemp As New DataTable
                    Dim lnqtemp As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
                    Dim sql As String = "select tb_filter_data.id from tb_filter_data inner join tb_shop  on tb_shop.id = tb_filter_data.shop_id "
                    sql += " where convert(varchar(8), service_date,112) = '" & Request("vDate") & "'"
                    If Request("LocationCode") <> "" Then
                        sql += " and shop_code ='" & Request("LocationCode") & "'"
                    End If
                    sql += " and tb_filter_id ='" & txtID.Text & "'"

                    dttemp = lnqtemp.GetListBySql(sql, trans.Trans)
                    If dttemp.Rows.Count = 0 Then
                        lblNotFound.Visible = True
                        lblDesc.Text = ""
                        Exit Sub
                    Else
                        lblNotFound.Visible = False
                    End If

                    Dim dt As New DataTable
                    Dim lnq As New CenLinqDB.TABLE.TbFilterShopCenLinqDB
                    sql = "select fs.*"
                    sql += " from tb_filter_shop fs "
                    sql += " inner join tb_shop s on s.id=fs.tb_shop_id "
                    sql += " where fs.tb_filter_id = '" & txtID.Text & "' and s.shop_code like '%" & Request("LocationCode") & "%'"
                    dt = lnq.GetListBySql(sql, trans.Trans)   '.GetDataList("", "", trans.Trans)
                    If dt.Rows.Count = 0 Then
                        lblNotFound.Visible = True
                        lblDesc.Text = ""
                    Else
                        lblNotFound.Visible = False
                    End If

                    SetDataList(txtID.Text, dt, Request("vDate"))
                    trans.CommitTransaction()
                    dt.Dispose()
                    lnq = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub FillData(ByVal vID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
        para = eng.GetFilterTemplatePara(vID, trans)
        trans.CommitTransaction()
        txtID.Text = para.ID
        lblTemplateName.Text = para.FILTER_NAME
        If para.TARGET_UNLIMITED = "Y" Then
            lblTarget.Text = "Target : Unlimited"
        Else
            lblTarget.Text = "Target : " & para.TARGET
        End If
        para = Nothing
        eng = Nothing
    End Sub

    Private Sub SetDataList(ByVal vID As Long, ByVal FilterShopDT As DataTable, ByVal SearchDate As String)
        Dim eng As New VerifyDataENG

        If FilterShopDT.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' class='mGrid' >")
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td width='20px' >&nbsp;</td>")
            ret.Append("        <td align='center' >Type Service</td>")
            ret.Append("        <td width='50px' align='center' >Regis</td>")
            ret.Append("        <td width='50px' align='center' >% Target</td>")
            ret.Append("        <td width='50px' align='center' >No. Target</td>")
            ret.Append("        <td width='50px' align='center' >Data For Send</td>")
            ret.Append("        <td width='50px' align='center' >Send Complete</td>")
            ret.Append("        <td width='50px' align='center' >Return Result</td>")
            ret.Append("        <td width='50px' align='center' >Status</td>")
            ret.Append("        <td width='150px' align='center' >Detail</td>")
            ret.Append("    </tr>")

            For i As Integer = 0 To FilterShopDT.Rows.Count - 1
                Dim Sdr As DataRow = FilterShopDT.Rows(i)

                Dim shPara As New CenParaDB.TABLE.TbShopCenParaDB
                Dim shEng As New Engine.Configuration.MasterENG
                shPara = shEng.GetShopPara(Sdr("tb_shop_id"))
                ret.Append("    <tr style='background: #B7D575 repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & (i + 1) & "</td>")
                ret.Append("        <td align='left' colspan='9' >" & shPara.SHOP_NAME_EN & "</td>")
                ret.Append("    </tr>")

                Dim dt As New DataTable
                dt = eng.GetVerifyDataList(vID, Sdr("tb_shop_id"), SearchDate)
                If dt.Rows.Count > 0 Then
                    Dim totRegis As Long = 0
                    Dim totTargetPer As Long = 0
                    Dim totNoTarget As Long = 0
                    Dim totDataForSend As Long = 0
                    Dim totSendComplete As Long = 0
                    Dim totReturnResult As Long = 0

                    For Each dr As DataRow In dt.Rows
                        ret.Append("    <tr style='background-color:white' >")
                        ret.Append("        <td >&nbsp;</td>")
                        ret.Append("        <td align='left' >&nbsp;" & dr("item_name") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & FormatNumber(Val(dr("regis") & ""), 0) & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("target_percent") & " %</td>")
                        If dr("target_unlimited").ToString = "Y" Then
                            ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                        Else
                            ret.Append("        <td align='center' >&nbsp;" & Math.Round(Convert.ToDouble(Val(dr("notarget") & ""))) & "</td>")
                        End If
                        ret.Append("        <td align='center' >&nbsp;" & dr("data_for_send") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("send_complete") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("return_result") & "</td>")

                        Dim vStatus As String = ""
                        If Convert.ToDouble(dr("return_result")) <> Math.Round(Convert.ToDouble(dr("send_complete"))) Then
                            vStatus = "Sending"
                        Else
                            vStatus = "Send Complete"
                        End If
                        If Convert.ToDouble(dr("return_result")) = 0 Then
                            vStatus = "Sending"
                        End If
                        ret.Append("        <td align='center' >&nbsp;" & vStatus & "</td>")
                        ret.Append("        <td align='center' >")
                        ret.Append("            <input type='button' class='formDialog' value=' Detail ' OnClick='OpenResultWindow(" & vID & ", " & Sdr("tb_shop_id") & "," & dr("item_id") & "," & SearchDate & ")' />")
                        ret.Append("        </td>")
                        ret.Append("    </tr>")
                        totRegis += Convert.ToInt64(Val(dr("Regis") & ""))
                        totTargetPer += Convert.ToInt64(dr("target_percent"))
                        totNoTarget += Math.Round(Convert.ToDouble(Val(dr("notarget") & "")))
                        totDataForSend += Convert.ToInt64(dr("data_for_send"))
                        totSendComplete += Convert.ToInt64(dr("send_complete"))
                        totReturnResult += Convert.ToInt64(dr("return_result"))
                    Next
                    'Total Row By Shop
                    ret.Append("    <tr style='background-color:gray' >")
                    ret.Append("        <td align='center' colspan='2' >&nbsp;Total</td>")
                    ret.Append("        <td align='center' >&nbsp;" & FormatNumber(totRegis, 0) & "</td>")
                    ret.Append("        <td align='center' >&nbsp;" & totTargetPer & " %</td>")
                    If dt.Rows(0)("target_unlimited").ToString = "Y" Then
                        ret.Append("        <td align='center' >&nbsp;Unlimited</td>")
                    Else
                        ret.Append("        <td align='center' >&nbsp;" & totNoTarget & "</td>")
                    End If
                    ret.Append("        <td align='center' >&nbsp;" & totDataForSend & "</td>")
                    ret.Append("        <td align='center' >&nbsp;" & totSendComplete & "</td>")
                    ret.Append("        <td align='center' >&nbsp;" & totReturnResult & "</td>")
                    ret.Append("        <td align='center' colspan='2' >&nbsp;</td>")
                    ret.Append("    </tr>")
                End If
            Next
            ret.Append("</table>")

            lblDesc.Text = ret.ToString
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSearchDate.DateValue.Year = 1 Then
            Config.SetAlert("กรุณาเลือกวันที่", Me)
            Exit Sub
        End If
        If txtSearchDate.DateValue >= Today.Date Then
            Config.SetAlert("กรุณาเลือกวันที่ย้อนหลังอย่างน้อย 1 วัน", Me)
            Exit Sub
        End If
        'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

        'Dim eng As New Engine.CSI.FilterTemplateENG
        'Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
        'para = eng.GetFilterTemplatePara(txtID.Text, trans)
        'trans.CommitTransaction()
        'txtID.Text = para.ID

        'SetDataList(para.ID, para.CHILD_TB_FILTER_SHOP_tb_filter_id, txtSearchDate.GetDateCondition)

        Response.Redirect("../CSIWebApp/frmVerifyData.aspx?rnd=" & Rnd() & "&id=" & txtID.Text & "&vDate=" & txtSearchDate.GetDateCondition & "&LocationCode=" & txtSearchLocationCode.Text & "&strDate=" & txtSearchDate.TxtBox.Text)
    End Sub
End Class
