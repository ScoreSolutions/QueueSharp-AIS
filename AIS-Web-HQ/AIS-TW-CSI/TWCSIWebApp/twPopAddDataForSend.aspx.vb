Imports System.Data
Partial Class TWCSIWebApp_twPopAddDataForSend
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("id") IsNot Nothing Then
            lblID.Text = Request("id")
            lblServiceID.Text = Request("ServiceID")
            lblShopID.Text = Request("ShopID")
            lblDate.Text = Request("vDate")

            Dim ret As String = ""
            Dim eng As New Engine.CSI.VerifyDataENG
            Dim dt As New DataTable
            dt = eng.GetNoResultData(lblDate.Text, lblShopID.Text, lblID.Text, lblServiceID.Text)
            If dt.Rows.Count > 0 Then
                ret += "<center>"
                ret += "<table border='1' cellpadding='0' cellspacing='0' width='200px' class='mGrid' >"
                ret += "    <tr style='background: #B7D575 repeat-x top;font-weight: bold;'>"
                ret += "        <td align = 'center'>Mobile No</td>"
                ret += "    </tr>"
                For Each dr As DataRow In dt.Rows
                    ret += "    <tr>"
                    ret += "        <td align = 'center'>" & dr("mobile_no") & "</td>"
                    ret += "    </tr>"
                Next
                ret += "</table>"
                ret += "</center>"

                lblMobileList.Text = ret
            End If

            If ret.Trim = "" Then
                lblMobileList.Text = "No Data Found"
            End If
        End If
    End Sub
End Class
