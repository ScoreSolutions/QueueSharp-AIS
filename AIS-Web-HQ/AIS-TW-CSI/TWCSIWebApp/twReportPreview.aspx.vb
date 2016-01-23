Imports System.Data
Imports System.Globalization
Imports System.IO
Imports Engine.Common

Partial Class TWCSIWebApp_twReportPreview
    Inherits System.Web.UI.Page

    Private Function GetWhText() As String
        Dim whText As String = "1=1"
        If Request("DateFrom") IsNot Nothing Then
            whText += " and convert(varchar(8),fd.send_time,112)>='" & Request("DateFrom").Trim & "'"
        End If
        If Request("DateTo") IsNot Nothing Then
            whText += " and convert(varchar(8),fd.send_time,112)<='" & Request("DateTo").Trim & "'"
        End If
        If Request("RegionCode") IsNot Nothing Then
            whText += " and L.region_code='" & Request("RegionCode") & "'"
        End If
        If Request("ProvinceCode") IsNot Nothing Then
            whText += " and L.province_code='" & Request("ProvinceCode") & "'"
        End If
        If Request("LocationCode") IsNot Nothing Then
            whText += " and L.location_code='" & Request("LocationCode") & "'"
        End If
        If Request("OrderTypeID") IsNot Nothing Then
            whText += " and fd.order_type_name = (select top 1 order_type_name from tw_sff_order_type where id='" & Request("OrderTypeID") & "')"
        End If
        If Request("TemplateID") IsNot Nothing Then
            whText += " and fd.tw_filter_id in (" & Request("TemplateID") & ")"
        End If
        If Request("Status") IsNot Nothing Then
            Dim tmpSt() As String = Split(Request("Status"), ",")
            Dim tmp As String = ""
            For Each t As String In tmpSt
                If tmp = "" Then
                    tmp = "'" & t & "'"
                Else
                    tmp += ",'" & t & "'"
                End If
            Next
            whText += " and fd.result_status in (" & tmp & ")"
        End If
        If Request("NetworkType") IsNot Nothing Then
            whText += " and fd.network_type = '" & Request("NetworkType") & "'"
        End If
        Return whText
    End Function

    Private Sub SearchDetailData()
        Dim eng As New Engine.CSI.CSIReportsENG
        Dim dt As New DataTable
        dt = eng.GetTWDetailDataList(GetWhText)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            'แสดงเงื่อนไขตามที่เลือก
            ret.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'  >")
            ret.Append("    <tr>")
            ret.Append("        <td width='100px' align='right'>Date Between :&nbsp;&nbsp;</td>")
            ret.Append("        <td>" & FunctionEng.cStrToDate3(Request("DateFrom")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & FunctionEng.cStrToDate3(Request("DateTo")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & "</td>")
            ret.Append("    </tr>")
            If Request("OrderTypeID") IsNot Nothing Then
                ret.Append("    <tr>")
                Dim lnq As New CenLinqDB.TABLE.TwSffOrderTypeCenLinqDB
                lnq.GetDataByPK(Request("OrderTypeID"), trans.Trans)
                ret.Append("        <td align='right'>Order Type :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & lnq.ORDER_TYPE_NAME & "</td>")
                ret.Append("    </tr>")
                lnq = Nothing
            End If
            If Request("Status") IsNot Nothing Then
                ret.Append("    <tr>")
                If Request("Status").ToString = "2" Then
                    ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>Complete</td>")
                Else
                    ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>Incomplete</td>")
                End If
                ret.Append("    </tr>")
            End If
            If Request("NetworkType") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Network Type :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("NetworkType") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("RegionCode") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Region :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("RegionCode") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("ProvinceCode") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Province :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("ProvinceCode") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("LocationCode") IsNot Nothing Then
                ret.Append("    <tr>")
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                lnq.ChkDataByLOCATION_CODE(Request("LocationCode"), trans.Trans)
                ret.Append("        <td align='right'>Location :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & lnq.LOCATION_NAME_TH & "</td>")
                ret.Append("    </tr>")
                lnq = Nothing
            End If


            If Request("TemplateID") IsNot Nothing Then
                Dim f As Integer = 0
                For Each fID As String In Request("TemplateID").Split(",")

                    Dim fPara As New CenParaDB.TABLE.TwFilterCenParaDB
                    Dim fEng As New Engine.CSI.FilterTemplateENG
                    fPara = fEng.GetTWFilterTemplatePara(Convert.ToInt64(fID), trans)
                    ret.Append("    <tr>")
                    If f = 0 Then
                        ret.Append("        <td align='right'>Template :&nbsp;&nbsp;</td>")
                    Else
                        ret.Append("        <td>&nbsp;</td>")
                    End If
                    ret.Append("        <td>" & fPara.FILTER_NAME & "</td>")
                    ret.Append("    </tr>")
                    fPara = Nothing
                    fEng = Nothing
                    f += 1
                Next
            End If
            ret.Append("</table>")
            trans.CommitTransaction()

            ret.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' class='mGrid' >")
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >Complete Datetime</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >Sent Datetime</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >ATSR Call Time</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Mobile No</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Result</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Region</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Province</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Location Code</td>")
            ret.Append("        <td align='center' width='120px' style='color: #ffffff;' >Location Name</td>")
            ret.Append("        <td align='center' width='150px' style='color: #ffffff;' >Name</td>")
            ret.Append("        <td align='center' width='100px' style='color: #ffffff;' >Order Type</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Template</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Network Type</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >NPS Score</td>")
            ret.Append("    </tr>")

            For Each dr As DataRow In dt.Rows
                ret.Append("    <tr>")
                If Convert.IsDBNull(dr("end_time")) = False Then
                    ret.Append("        <td align='center' >" & Convert.ToDateTime(dr("end_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH")) & "</td>")
                Else
                    ret.Append("        <td align='left' >&nbsp;</td>")
                End If
                If Convert.IsDBNull(dr("send_time")) = False Then
                    ret.Append("        <td align='center' >" & Convert.ToDateTime(dr("send_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH")) & "</td>")
                Else
                    ret.Append("        <td align='left' >&nbsp;</td>")
                End If
                If Convert.IsDBNull(dr("atsr_call_time")) = False Then
                    ret.Append("        <td align='center' >" & Convert.ToDateTime(dr("atsr_call_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH")) & "</td>")
                Else
                    ret.Append("        <td align='left' >&nbsp;</td>")
                End If
                ret.Append("        <td align='center' >&nbsp;" & dr("mobile_no") & "</td>")
                ret.Append("        <td align='center' >" & dr("result") & "</td>")
                ret.Append("        <td align='center' >" & dr("region_code") & "</td>")
                ret.Append("        <td align='center' >" & dr("province_code") & "</td>")
                ret.Append("        <td align='center' >" & dr("location_code") & "</td>")
                ret.Append("        <td align='left' >" & dr("location_name_th") & "</td>")
                ret.Append("        <td align='left' >" & dr("customer_name") & "</td>")
                ret.Append("        <td align='left' >" & dr("order_type_name") & "</td>")
                ret.Append("        <td align='left' >" & dr("filter_name") & "</td>")
                ret.Append("        <td align='center' >" & dr("network_type") & "</td>")
                ret.Append("        <td align='center' >")
                If Convert.ToInt64(dr("nps_score")) <= -1 Then
                    ret.Append("        &nbsp;")
                Else
                    ret.Append("        " & dr("nps_score"))
                End If
                ret.Append("        </td>")
                ret.Append("    </tr>")
            Next
            ret.Append("</table>")
            lblRowCount.Text = "ค้นพบ " & dt.Rows.Count & " รายการ"

            lblReportDesc.Text = ret.ToString
            ret = Nothing
            dt.Dispose()
            lblerror.Visible = False
        Else
            lblReportDesc.Text = ""
            imgExport.Visible = False
            likExport.Visible = False
        End If
    End Sub

    Private Sub SearchCSIData()
        Dim ret As New StringBuilder
        Dim eng As New Engine.CSI.CSIReportsENG
        Dim dt As New DataTable
        dt = eng.GetTWCSIDataList(GetWhText)
        If dt.Rows.Count > 0 Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            'แสดงเงื่อนไขตามที่เลือก
            ret.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'  >")
            ret.Append("    <tr>")
            ret.Append("        <td width='100px' align='right'>Date Between :&nbsp;&nbsp;</td>")
            ret.Append("        <td>" & FunctionEng.cStrToDate3(Request("DateFrom")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & FunctionEng.cStrToDate3(Request("DateTo")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & "</td>")
            ret.Append("    </tr>")
            If Request("OrderTypeID") IsNot Nothing Then
                ret.Append("    <tr>")
                Dim lnq As New CenLinqDB.TABLE.TwSffOrderTypeCenLinqDB
                lnq.GetDataByPK(Request("OrderTypeID"), trans.Trans)
                ret.Append("        <td align='right'>Order Type :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & lnq.ORDER_TYPE_NAME & "</td>")
                ret.Append("    </tr>")
                lnq = Nothing
            End If
            If Request("Status") IsNot Nothing Then
                ret.Append("    <tr>")
                If Request("Status").ToString = "2" Then
                    ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>Complete</td>")
                Else
                    ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>Incomplete</td>")
                End If
                ret.Append("    </tr>")
            End If
            If Request("NetworkType") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Network Type :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("NetworkType") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("RegionCode") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Region :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("RegionCode") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("ProvinceCode") IsNot Nothing Then
                ret.Append("    <tr>")
                ret.Append("        <td align='right'>Province :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & Request("ProvinceCode") & "</td>")
                ret.Append("    </tr>")
            End If

            If Request("LocationCode") IsNot Nothing Then
                ret.Append("    <tr>")
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                lnq.ChkDataByLOCATION_CODE(Request("LocationCode"), trans.Trans)
                ret.Append("        <td align='right'>Location :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & lnq.LOCATION_NAME_TH & "</td>")
                ret.Append("    </tr>")
                lnq = Nothing
            End If

           
            If Request("TemplateID") IsNot Nothing Then
                Dim f As Integer = 0
                For Each fID As String In Request("TemplateID").Split(",")

                    Dim fPara As New CenParaDB.TABLE.TwFilterCenParaDB
                    Dim fEng As New Engine.CSI.FilterTemplateENG
                    fPara = fEng.GetTWFilterTemplatePara(Convert.ToInt64(fID), trans)
                    ret.Append("    <tr>")
                    If f = 0 Then
                        ret.Append("        <td align='right'>Template :&nbsp;&nbsp;</td>")
                    Else
                        ret.Append("        <td>&nbsp;</td>")
                    End If
                    ret.Append("        <td>" & fPara.FILTER_NAME & "</td>")
                    ret.Append("    </tr>")
                    fPara = Nothing
                    fEng = Nothing
                    f += 1
                Next
            End If
            ret.Append("</table>")
            trans.CommitTransaction()


            ret.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' class='mGrid' >")
            'สร้างชื่อคอลัมน์ก่อน
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' width='30px' style='color: #ffffff;' >No</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >Location</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >Province</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >Region</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Location Name</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Location Type</td>")
            ret.Append("        <td align='center' width='60px' style='color: #ffffff;' >Segment</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >ผลสำรวจ</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >CSI ดีมาก</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >CSI ดี</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >CSI ควรปรับปรุง</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >CSI Score</td>")
            ret.Append("        <td align='center' width='80px' style='color: #ffffff;' >% CSI</td>")
            ret.Append("    </tr>")

            Dim rDt As New DataTable
            dt.DefaultView.Sort = "region_code"
            rDt = dt.DefaultView.ToTable(True, "region_code").Copy
            If rDt.Rows.Count > 0 Then
                Dim totResultAll As Long = 0
                Dim totVeryGoodAll As Long = 0
                Dim totGoodAll As Long = 0
                Dim totAdjustAll As Long = 0

                For Each rDr As DataRow In rDt.Rows
                    Dim totResult As Long = 0
                    Dim totVeryGood As Long = 0
                    Dim totGood As Long = 0
                    Dim totAdjust As Long = 0

                    dt.DefaultView.Sort = "province_code,location_name_th"
                    dt.DefaultView.RowFilter = "region_code = '" & rDr("region_code") & "'"
                    Dim dv As DataView = dt.DefaultView
                    Dim i As Integer = 1
                    For Each dr As DataRowView In dv
                        Dim vTotal As Long = Convert.ToInt64(dr("result_very_good")) + Convert.ToInt64(dr("result_good")) + Convert.ToInt64(dr("result_adjust"))
                        Dim vCSIScore As Double = Convert.ToInt64(dr("result_very_good")) + (Convert.ToInt64(dr("result_good")) / 2)
                        Dim pCSI As Double = 0
                        If vTotal > 0 Then
                            pCSI = (vCSIScore * 100) / vTotal
                        End If
                        ret.Append("    <tr>")
                        ret.Append("        <td align='center'>&nbsp;" & i & ".</td>")
                        ret.Append("        <td align='center'>" & dr("location_code") & "</td>")
                        ret.Append("        <td align='center'>" & dr("province_code") & "</td>")
                        ret.Append("        <td align='center'>" & dr("region_code") & "</td>")
                        ret.Append("        <td>" & dr("location_name_th") & "</td>")
                        ret.Append("        <td align='center'>" & dr("location_Type") & "</td>")
                        ret.Append("        <td align='center'>" & dr("location_segment") & "</td>")
                        ret.Append("        <td align='center'>" & vTotal & "</td>")
                        ret.Append("        <td align='center'>" & dr("result_very_good") & "</td>")
                        ret.Append("        <td align='center'>" & dr("result_good") & "</td>")
                        ret.Append("        <td align='center'>" & dr("result_adjust") & "</td>")
                        ret.Append("        <td align='center'>" & Math.Round(vCSIScore, 2) & "</td>")
                        ret.Append("        <td align='center'>" & Math.Round(pCSI) & "%</td>")
                        ret.Append("    </tr>")

                        totResult += vTotal
                        totVeryGood += Convert.ToInt64(dr("result_very_good"))
                        totGood += Convert.ToInt64(dr("result_good"))
                        totAdjust += Convert.ToInt64(dr("result_adjust"))
                        i += 1
                    Next
                    dt.DefaultView.RowFilter = ""

                    Dim totCSIScore As Double = totVeryGood + (totGood / 2)
                    Dim totPCSI As Double = 0
                    If totResult > 0 Then
                        totPCSI = (totCSIScore * 100) / totResult
                    End If

                    'Summary by Region
                    ret.Append("    <tr style='background: gray repeat-x top;font-weight: bold;' >")
                    'ret.Append("        <td align='center'>&nbsp;</td>")
                    'ret.Append("        <td align='center'>&nbsp;</td>")
                    'ret.Append("        <td align='center'>&nbsp;</td>")
                    ret.Append("        <td align='center' colspan='4'>" & rDr("region_code") & "</td>")
                    ret.Append("        <td>&nbsp;</td>")
                    ret.Append("        <td align='center'>&nbsp;</td>")
                    ret.Append("        <td align='center'>&nbsp;</td>")
                    ret.Append("        <td align='center'>" & totResult & "</td>")
                    ret.Append("        <td align='center'>" & totVeryGood & "</td>")
                    ret.Append("        <td align='center'>" & totGood & "</td>")
                    ret.Append("        <td align='center'>" & totAdjust & "</td>")
                    ret.Append("        <td align='center'>" & Math.Round(totCSIScore, 2) & "</td>")
                    ret.Append("        <td align='center'>" & Math.Round(totPCSI) & "%</td>")
                    ret.Append("    </tr>")

                    totResultAll += totResult
                    totVeryGoodAll += totVeryGood
                    totGoodAll += totGood
                    totAdjustAll += totAdjust

                Next

                Dim totCSIScoreAll As Double = totVeryGoodAll + (totGoodAll / 2)
                Dim totPCSIAll As Double = 0
                If totResultAll > 0 Then
                    totPCSIAll = (totCSIScoreAll * 100) / totResultAll
                End If

                'Summary All
                ret.Append("    <tr style='background: #2E9AFE repeat-x top;font-weight: bold;' >")
                'ret.Append("        <td align='center'>&nbsp;</td>")
                'ret.Append("        <td align='center'>&nbsp;</td>")
                'ret.Append("        <td align='center'>&nbsp;</td>")
                ret.Append("        <td align='center' colspan='4'>Over All</td>")
                ret.Append("        <td>&nbsp;</td>")
                ret.Append("        <td align='center'>&nbsp;</td>")
                ret.Append("        <td align='center'>&nbsp;</td>")
                ret.Append("        <td align='center'>" & totResultAll & "</td>")
                ret.Append("        <td align='center'>" & totVeryGoodAll & "</td>")
                ret.Append("        <td align='center'>" & totGoodAll & "</td>")
                ret.Append("        <td align='center'>" & totAdjustAll & "</td>")
                ret.Append("        <td align='center'>" & Math.Round(totCSIScoreAll, 2) & "</td>")
                ret.Append("        <td align='center'>" & Math.Round(totPCSIAll) & "%</td>")
                ret.Append("    </tr>")
            End If
            rDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
        Else
            imgExport.Visible = False
            likExport.Visible = False
        End If
        dt.Dispose()

        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
            'lblRowCount.Text = "ค้นพบ " & dt.Rows.Count & " รายการ"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request("ReportName") = "Detail" Then
                lblReportName.Text = "Detail Report"
                SearchDetailData()
            ElseIf Request("ReportName") = "CSI" Then
                lblReportName.Text = "CSI Report"
                SearchCSIData()
            ElseIf Request("ReportName") = "NPSSCORE" Then
                lblReportName.Text = "NPS SCORE Report"
                SearchNPSSCOREData()
            End If
        End If
    End Sub

    Protected Sub likExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles likExport.Click
        ExportData("application/vnd.xls", Replace(lblReportName.Text, " ", "_") & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Protected Sub imgExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        ExportData("application/vnd.xls", Replace(lblReportName.Text, " ", "_") & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Response.ClearContent()
        Response.Charset = "utf-8"
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = _contentType
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        frm.Attributes("runat") = "server"
        frm.Controls.Add(lblReportDesc)
        lblReportDesc.RenderControl(htw)

        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

    Function CheckRoundDigit(ByVal number As Double) As Boolean
        Try
            Dim str As String() = number.ToString.Split(".")
            If str.Length > 1 Then
                If CInt(str(1)) = 5 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    

    Private Sub SearchNPSSCOREData()
        Dim sEng As New Engine.Configuration.MasterENG
        Dim sDt As New DataTable
        Dim sWh As String = " 1 = 1 "
        If Request("OrderTypeID") IsNot Nothing Then
            sWh += " and id= '" & Request("OrderTypeID") & "'"
        End If
        sDt = sEng.GetTWSffOrderTypeList(sWh)

        Dim shWh As String = " "
        If Request("RegionCode") IsNot Nothing Then
            shWh += " and region_code = '" & Request("RegionCode") & "'"

            If Request("ProvinceCode") IsNot Nothing Then
                shWh += " and province_code = '" & Request("RegionCode") & "'"

                If Request("LocationCode") IsNot Nothing Then
                    shWh += " and location_code = '" & Request("LocationCode") & "'"
                End If
            End If
        End If
        
        Dim shDt As New DataTable
        Dim shEng As New Engine.Configuration.MasterENG
        shDt = shEng.GetTWShopList(shWh, "")

        If sDt.Rows.Count > 0 Then
            Dim eng As New Engine.CSI.CSIReportsENG
            Dim dt As New DataTable
            dt = eng.GetTWNPSSCOREDataList(GetWhText)
            If dt.Rows.Count > 0 Then
                Dim ret As New StringBuilder
                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                'แสดงเงื่อนไขตามที่เลือก
                ret.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'  >")
                ret.Append("    <tr>")
                ret.Append("        <td width='100px' align='right'>Date Between :&nbsp;&nbsp;</td>")
                ret.Append("        <td>" & FunctionEng.cStrToDate3(Request("DateFrom")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & FunctionEng.cStrToDate3(Request("DateTo")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & "</td>")
                ret.Append("    </tr>")
                If Request("OrderTypeID") IsNot Nothing Then
                    ret.Append("    <tr>")
                    Dim lnq As New CenLinqDB.TABLE.TwSffOrderTypeCenLinqDB
                    lnq.GetDataByPK(Request("OrderTypeID"), trans.Trans)
                    ret.Append("        <td align='right'>Order Type :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>" & lnq.ORDER_TYPE_NAME & "</td>")
                    ret.Append("    </tr>")
                    lnq = Nothing
                End If
                If Request("Status") IsNot Nothing Then
                    ret.Append("    <tr>")
                    If Request("Status").ToString = "2" Then
                        ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                        ret.Append("        <td>Complete</td>")
                    Else
                        ret.Append("        <td align='right'>Status :&nbsp;&nbsp;</td>")
                        ret.Append("        <td>Incomplete</td>")
                    End If
                    ret.Append("    </tr>")
                End If
                If Request("NetworkType") IsNot Nothing Then
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>Network Type :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>" & Request("NetworkType") & "</td>")
                    ret.Append("    </tr>")
                End If

                If Request("RegionCode") IsNot Nothing Then
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>Region :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>" & Request("RegionCode") & "</td>")
                    ret.Append("    </tr>")
                End If

                If Request("ProvinceCode") IsNot Nothing Then
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>Province :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>" & Request("ProvinceCode") & "</td>")
                    ret.Append("    </tr>")
                End If

                If Request("LocationCode") IsNot Nothing Then
                    ret.Append("    <tr>")
                    Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                    lnq.ChkDataByLOCATION_CODE(Request("LocationCode"), trans.Trans)
                    ret.Append("        <td align='right'>Location :&nbsp;&nbsp;</td>")
                    ret.Append("        <td>" & lnq.LOCATION_NAME_TH & "</td>")
                    ret.Append("    </tr>")
                    lnq = Nothing
                End If

                If Request("TemplateID") IsNot Nothing Then
                    Dim f As Integer = 0
                    For Each fID As String In Request("TemplateID").Split(",")

                        Dim fPara As New CenParaDB.TABLE.TwFilterCenParaDB
                        Dim fEng As New Engine.CSI.FilterTemplateENG
                        fPara = fEng.GetTWFilterTemplatePara(Convert.ToInt64(fID), trans)
                        ret.Append("    <tr>")
                        If f = 0 Then
                            ret.Append("        <td align='right'>Template :&nbsp;&nbsp;</td>")
                        Else
                            ret.Append("        <td>&nbsp;</td>")
                        End If
                        ret.Append("        <td>" & fPara.FILTER_NAME & "</td>")
                        ret.Append("    </tr>")
                        fPara = Nothing
                        fEng = Nothing
                        f += 1
                    Next
                End If

                ret.Append("</table>")
                trans.CommitTransaction()

                ret.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' class='mGrid' >")
                ret.Append("    <tr style='background: yellow repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >No</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Location</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Province</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Region</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Location Name</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Location Type</td>")
                ret.Append("        <td align='center'  rowspan='2' style='color: #000000;' >Segment</td>")
                ret.Append("        <td align='center' colspan='22'  style='color: #000000;' >NPS Score</td>")
                ret.Append("    </tr>")
                ret.Append("    <tr style='background: yellow repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;0</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;1</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;2</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;3</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;4</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;5</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;6</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;0-6</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;%</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;% 0-6</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;7</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;8</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;7-8</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;%</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;% 7-8</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;9</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;10</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;9-10</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;%</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >&nbsp;% 9-10</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >Grand Total</td>")
                ret.Append("        <td align='center' width='50px' style='color: #000000;' >NPS</td>")
                ret.Append("    </tr>")

                Dim _all_sum0 As Integer = 0
                Dim _all_sum1 As Integer = 0
                Dim _all_sum2 As Integer = 0
                Dim _all_sum3 As Integer = 0
                Dim _all_sum4 As Integer = 0
                Dim _all_sum5 As Integer = 0
                Dim _all_sum6 As Integer = 0
                Dim _all_sum7 As Integer = 0
                Dim _all_sum8 As Integer = 0
                Dim _all_sum9 As Integer = 0
                Dim _all_sum10 As Integer = 0

                Dim rDt As New DataTable
                dt.DefaultView.Sort = "region_code,shop_name_en"
                rDt = dt.DefaultView.ToTable(True, "region_code").Copy
                If rDt.Rows.Count > 0 Then
                    For Each rDr As DataRow In rDt.Rows
                        dt.DefaultView.RowFilter = "region_code='" & rDr("region_code") & "'"

                        Dim _sum0 As Integer = 0
                        Dim _sum1 As Integer = 0
                        Dim _sum2 As Integer = 0
                        Dim _sum3 As Integer = 0
                        Dim _sum4 As Integer = 0
                        Dim _sum5 As Integer = 0
                        Dim _sum6 As Integer = 0
                        Dim _sum7 As Integer = 0
                        Dim _sum8 As Integer = 0
                        Dim _sum9 As Integer = 0
                        Dim _sum10 As Integer = 0
                        If dt.DefaultView.Count > 0 Then
                            Dim i As Integer = 1
                            For Each dr As DataRowView In dt.DefaultView
                                Dim grandTo As Integer = CInt(dr("GrandTo"))
                                Dim sum0_6 As Integer = CInt(dr("score_0")) + CInt(dr("score_1")) + _
                                    CInt(dr("score_2")) + CInt(dr("score_3")) + CInt(dr("score_4")) + _
                                    CInt(dr("score_5")) + CInt(dr("score_6"))
                                Dim sum7_8 As Integer = CInt(dr("score_7")) + CInt(dr("score_8"))
                                Dim sum9_10 As Integer = CInt(dr("score_9")) + CInt(dr("score_10"))
                                Dim percent0_6 As Double = Math.Round((sum0_6 / grandTo) * 100, 2)
                                Dim percent7_8 As Double = Math.Round((sum7_8 / grandTo) * 100, 2)
                                Dim percent9_10 As Double = Math.Round((sum9_10 / grandTo) * 100, 2)
                                Dim round0_6 As Long = IIf(CheckRoundDigit(percent0_6) = True, Math.Round(percent0_6 + 0.01), Math.Round(percent0_6))
                                Dim round7_8 As Long = IIf(CheckRoundDigit(percent7_8) = True, Math.Round(percent7_8 + 0.01), Math.Round(percent7_8))
                                Dim round9_10 As Integer = IIf(CheckRoundDigit(percent9_10) = True, Math.Round(percent9_10 + 0.01), Math.Round(percent9_10))
                                Dim NPS As Integer = round9_10 - round0_6

                                _sum0 += CInt(dr("score_0"))
                                _sum1 += CInt(dr("score_1"))
                                _sum2 += CInt(dr("score_2"))
                                _sum3 += CInt(dr("score_3"))
                                _sum4 += CInt(dr("score_4"))
                                _sum5 += CInt(dr("score_5"))
                                _sum6 += CInt(dr("score_6"))
                                _sum7 += CInt(dr("score_7"))
                                _sum8 += CInt(dr("score_8"))
                                _sum9 += CInt(dr("score_9"))
                                _sum10 += CInt(dr("score_10"))

                                ret.Append("    <tr>")
                                ret.Append("        <td align='center' width='30px' style='color: #000000;' >" & i & "</td>")
                                ret.Append("        <td align='center' width='150px' style='color: #000000;' >" & dr("shop_code").ToString & "</td>")
                                ret.Append("        <td align='center' width='150px' style='color: #000000;' >" & dr("province_code").ToString & "</td>")
                                ret.Append("        <td align='center' width='150px' style='color: #000000;' >" & dr("region_code").ToString & "</td>")
                                ret.Append("        <td align='left' width='250px' style='color: #000000;' >" & dr("shop_name_en").ToString & "</td>")
                                ret.Append("        <td align='left' width='250px' style='color: #000000;' >" & dr("location_type").ToString & "</td>")
                                ret.Append("        <td align='left' width='250px' style='color: #000000;' >" & dr("location_segment").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_0").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_1").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_2").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_3").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_4").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_5").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_6").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & sum0_6 & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & Format(percent0_6, "0.00") & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & round0_6 & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_7").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_8").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & sum7_8 & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & Format(percent7_8, "0.00") & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & round7_8 & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_9").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & dr("score_10").ToString & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & sum9_10 & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & Format(percent9_10, "0.00") & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & round9_10 & "%</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & grandTo & "</td>")
                                ret.Append("        <td align='right' width='50px' style='color: #000000;' >" & NPS & "%</td>")
                                ret.Append("    </tr>")

                                i += 1
                            Next
                        End If
                        dt.DefaultView.RowFilter = ""


                        Dim _grandTo As Integer = _sum0 + _sum1 + _sum2 + _sum3 + _sum4 + _sum5 + _sum6 + _sum7 + _sum8 + _sum9 + _sum10
                        Dim _sum0_6 As Integer = _sum0 + _sum1 + _sum2 + _sum3 + _sum4 + _sum5 + _sum6
                        Dim _sum7_8 As Integer = _sum7 + _sum8
                        Dim _sum9_10 As Integer = _sum9 + _sum10
                        Dim _percent0_6 As Double = Math.Round((_sum0_6 / _grandTo) * 100, 2)
                        Dim _percent7_8 As Double = Math.Round((_sum7_8 / _grandTo) * 100, 2)
                        Dim _percent9_10 As Double = Math.Round((_sum9_10 / _grandTo) * 100, 2)
                        'Dim _round0_6 As Integer = Math.Round(_percent0_6)
                        'Dim _round7_8 As Integer = Math.Round(_percent7_8)
                        'Dim _round9_10 As Integer = Math.Round(_percent9_10)
                        Dim _round0_6 As Long = IIf(CheckRoundDigit(_percent0_6) = True, Math.Round(_percent0_6 + 0.01), Math.Round(_percent0_6))
                        Dim _round7_8 As Long = IIf(CheckRoundDigit(_percent7_8) = True, Math.Round(_percent7_8 + 0.01), Math.Round(_percent7_8))
                        Dim _round9_10 As Long = IIf(CheckRoundDigit(_percent9_10) = True, Math.Round(_percent9_10 + 0.01), Math.Round(_percent9_10))
                        Dim _nps As Integer = _round9_10 - _round0_6

                        _all_sum0 += _sum0
                        _all_sum1 += _sum1
                        _all_sum2 += _sum2
                        _all_sum3 += _sum3
                        _all_sum4 += _sum4
                        _all_sum5 += _sum5
                        _all_sum6 += _sum6
                        _all_sum7 += _sum7
                        _all_sum8 += _sum8
                        _all_sum9 += _sum9
                        _all_sum10 += _sum10

                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
                        'Summary by Region
                        ret.Append("        <td align='center' colspan='7' style='color: #000000;' >" & rDr("region_code") & "</td>")
                        'ret.Append("        <td align='center'  style='color: #000000;' ></td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum0 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum1 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum2 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum3 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum4 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum5 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum6 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum0_6 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_percent0_6, "0.00") & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _round0_6 & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum7 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum8 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum7_8 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_percent7_8, "0.00") & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _round7_8 & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum9 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum10 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _sum9_10 & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_percent9_10, "0.00") & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _round9_10 & "%</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _grandTo & "</td>")
                        ret.Append("        <td align='right'  style='color: #000000;' >" & _nps & "%</td>")
                        ret.Append("    </tr>")
                    Next
                End If


                Dim _all_grandTo As Integer = _all_sum0 + _all_sum1 + _all_sum2 + _all_sum3 + _all_sum4 + _
                _all_sum5 + _all_sum6 + _all_sum7 + _all_sum8 + _all_sum9 + _all_sum10
                Dim _all_sum0_6 As Integer = _all_sum0 + _all_sum1 + _all_sum2 + _all_sum3 + _all_sum4 + _all_sum5 + _all_sum6
                Dim _all_sum7_8 As Integer = _all_sum7 + _all_sum8
                Dim _all_sum9_10 As Integer = _all_sum9 + _all_sum10
                Dim _all_percent0_6 As Double = Math.Round((_all_sum0_6 / _all_grandTo) * 100, 2)
                Dim _all_percent7_8 As Double = Math.Round((_all_sum7_8 / _all_grandTo) * 100, 2)
                Dim _all_percent9_10 As Double = Math.Round((_all_sum9_10 / _all_grandTo) * 100, 2)
                'Dim _all_round0_6 As Integer = Math.Round(_all_percent0_6)
                'Dim _all_round7_8 As Integer = Math.Round(_all_percent7_8)
                'Dim _all_round9_10 As Integer = Math.Round(_all_percent9_10)
                Dim _all_round0_6 As Long = IIf(CheckRoundDigit(_all_percent0_6) = True, Math.Round(_all_percent0_6 + 0.01), Math.Round(_all_percent0_6))
                Dim _all_round7_8 As Long = IIf(CheckRoundDigit(_all_percent7_8) = True, Math.Round(_all_percent7_8 + 0.01), Math.Round(_all_percent7_8))
                Dim _all_round9_10 As Long = IIf(CheckRoundDigit(_all_percent9_10) = True, Math.Round(_all_percent9_10 + 0.01), Math.Round(_all_percent9_10))
                Dim _all_nps As Integer = _all_round9_10 - _all_round0_6

                ret.Append("    <tr style='background: #2E9AFE repeat-x top;font-weight: bold;'>")
                'Summary all
                ret.Append("        <td align='center' colspan='7' style='color: #000000;' >Over All</td>")
                ' ret.Append("        <td align='center'  style='color: #000000;' ></td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum0 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum1 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum2 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum3 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum4 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum5 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum6 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum0_6 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_all_percent0_6, "0.00") & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_round0_6 & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum7 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum8 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum7_8 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_all_percent7_8, "0.00") & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_round7_8 & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum9 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum10 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_sum9_10 & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & Format(_all_percent9_10, "0.00") & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_round9_10 & "%</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_grandTo & "</td>")
                ret.Append("        <td align='right'  style='color: #000000;' >" & _all_nps & "%</td>")
                ret.Append("    </tr>")
                rDt.Dispose()

                ret.Append("</table>")
                lblRowCount.Text = "ค้นพบ " & dt.Rows.Count & " รายการ"

                lblReportDesc.Text = ret.ToString
                ret = Nothing
                dt = Nothing
                lblerror.Visible = False
            Else
                lblReportDesc.Text = ""
                imgExport.Visible = False
                likExport.Visible = False
            End If
        End If

        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
            'lblRowCount.Text = "ค้นพบ " & dt.Rows.Count & " รายการ"
        End If
    End Sub
End Class


