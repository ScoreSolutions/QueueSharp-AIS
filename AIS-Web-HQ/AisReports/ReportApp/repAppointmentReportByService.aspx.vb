Imports System.IO
Imports System.Data
Imports Engine.Common
Imports Engine.Reports
Imports CenParaDB.Common.Utilities.Constant

Partial Class ReportApp_repAppointmentReportByService
    Inherits System.Web.UI.Page

    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "AppointmentReportByService_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repProductivity.ExportData", Config.GetLoginHistoryID)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("ReportName") IsNot Nothing Then
            Title = Version
            ShowReport(Request("ReportName"))
        End If
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        
        Dim para As New CenParaDB.ReportCriteria.AppointmentReportByServicePara
        para.ShopID = Request("ShopID")
        para.DateFrom = Request("DateFrom")
        para.DateTo = Request("DateTo")
        Dim eng As New Engine.Reports.RepAppointmentReportByServiceENG
        Dim dt As New DataTable
        dt = eng.GetReportByDate(para)
        If dt.Rows.Count > 0 Then
            RenderReportByDate(dt)
        Else
            btnExport.Visible = False
        End If
        dt.Dispose()
        eng = Nothing
        para = Nothing
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "shop_name_en,appointment_date"
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Transaction Date/Time</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Date/Time</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Mobile Number</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Segment</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Status</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Remark</td>")
            ret.Append("    </tr>")

            Dim GcMobileNo As Integer = 0

            'Data Row By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim cMobileNo As Integer = 0
                dt.DefaultView.RowFilter = " shop_id='" & sDr("shop_id") & "'"
                For Each dr As DataRowView In dt.DefaultView
                    ret.Append("    <tr>")
                    ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center'>" & Convert.ToDateTime(dr("appointment_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                    ret.Append("        <td align='center'>" & Convert.ToDateTime(dr("transaction_date")).ToString("dd/MM/yyyy HH:mm", New Globalization.CultureInfo("en-US")) & "</td>")
                    ret.Append("        <td align='center'>" & Convert.ToDateTime(dr("appointment_date")).ToString("dd/MM/yyyy HH:mm", New Globalization.CultureInfo("en-US")) & "</td>")
                    ret.Append("        <td align='center'>" & GetAppointmentChannel(dr("appointment_channel")) & "</td>")
                    ret.Append("        <td align='center'>&nbsp;" & dr("mobile_no") & "</td>")
                    ret.Append("        <td align='center'>" & dr("segment") & "</td>")
                    ret.Append("        <td align='center'>" & dr("user_code") & "</td>")
                    ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                    ret.Append("        <td align='left'>" & dr("service_name_en") & "</td>")
                    ret.Append("        <td align='center'>" & dr("appointment_status") & "</td>")
                    ret.Append("        <td align='center'>&nbsp;</td>")
                    ret.Append("    </tr>")

                    cMobileNo += 1
                    GcMobileNo += 1
                Next

                ' Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >" & Format(cMobileNo, "#,##0") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GcMobileNo, "#,##0") & "</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Function GetAppointmentChannel(ByVal AppointmentChannel As String) As String
        Dim ret As String = ""
        Select Case AppointmentChannel.Trim
            Case TbAppointmentCustomer.AppointmentChannel.Kiosk
                ret = "Kiosk"
            Case TbAppointmentCustomer.AppointmentChannel.WebAppoint
                ret = "Web Site"
            Case TbAppointmentCustomer.AppointmentChannel.Mobile
                ret = "Mobile"
            Case TbAppointmentCustomer.AppointmentChannel.CallCenter
                ret = "Call Center"
        End Select

        Return ret
    End Function

End Class
